namespace UMLToMVCConverter
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using UMLToMVCConverter.CodeTemplates;
    using UMLToMVCConverter.ExtensionMethods;

    public class MvcProjectConfigurator : IMvcProjectConfigurator
    {
        private readonly string modelsFolderPath;
        private readonly string connectionString;
        private readonly string mvcProjectName;
        private readonly string mvcProjectFolderPath;
        private readonly IStartupCsConfigurator startupCsConfigurator;
        private IMigrationsManager migrationsManager;
        private IProjectBuilder projectBuilder;

        public MvcProjectConfigurator(string mvcProjectFolderPath, string connectionString)
        {
            Insist.IsNotNullOrWhiteSpace(mvcProjectFolderPath, nameof(mvcProjectFolderPath));
            Insist.IsNotNullOrWhiteSpace(connectionString, nameof(connectionString));

            this.mvcProjectFolderPath = mvcProjectFolderPath;
            this.modelsFolderPath = Path.Combine(this.mvcProjectFolderPath, @"Models");
            this.connectionString = connectionString;
            this.mvcProjectName = Path.GetFileName(this.mvcProjectFolderPath);

            this.startupCsConfigurator = new StartupCsConfigurator(this.mvcProjectFolderPath);
        }

        public void SetUpMvcProject(List<CodeTypeDeclaration> codeTypeDeclarations, string namespaceName)
        {
            Insist.IsNotNullOrWhiteSpace(namespaceName, nameof(namespaceName));

            var dbContextName = GetDbContextName(namespaceName);

            this.SetUpAppsettingsDbConnection(dbContextName);

            this.startupCsConfigurator.SetUpStartupCsDbContextUse(dbContextName);

            PrepareModelsFolder(this.modelsFolderPath);
            this.GenerateModels(codeTypeDeclarations);
            this.GenerateDbContextClass(codeTypeDeclarations, namespaceName);
            
            this.projectBuilder = new ProjectBuilder();
            var mvcProjectAssembly = this.projectBuilder.BuildProject(this.mvcProjectFolderPath);

            this.migrationsManager = new MigrationsManager(this.mvcProjectName, dbContextName, this.mvcProjectFolderPath);
            this.migrationsManager.AddAndRunMigrations(mvcProjectAssembly);
        }

        private void SetUpAppsettingsDbConnection(string contextName)
        {
            var appsettingJsonPath = Path.Combine(this.mvcProjectFolderPath, "appsettings.json");
            var appsettingsJsonContent = File.ReadAllText(appsettingJsonPath);
            var appsettingsJson = JObject.Parse(appsettingsJsonContent);

            var connectionStringConfig = new JObject
            {
                [contextName] = this.connectionString
            };

            var connectionStrings = new JProperty("ConnectionStrings", connectionStringConfig);

            appsettingsJson.AddOrUpdate(connectionStrings);

            var appsettingsJsonOutput = JsonConvert.SerializeObject(appsettingsJson, Formatting.Indented);

            File.WriteAllText(appsettingJsonPath, appsettingsJsonOutput);
        }

        private void GenerateModels(IEnumerable<CodeTypeDeclaration> codeTypeDeclarations)
        {
            var codeTypeDeclarationsList = codeTypeDeclarations.ToList();
            foreach (var ctd in codeTypeDeclarationsList)
            {
                var tmpl = new ModelClassTextTemplate(ctd, this.mvcProjectName);
                var fileContent = tmpl.TransformText();
                var filesOutputPath = Path.Combine(this.modelsFolderPath, ctd.Name + ".cs");
                File.WriteAllText(filesOutputPath, fileContent);
            }
        }

        private void GenerateDbContextClass(List<CodeTypeDeclaration> codeTypeDeclarations, string namespaceName)
        {
            var standaloneEntityTypes = codeTypeDeclarations.
                Where(i => !i.TypeAttributes.HasFlag(TypeAttributes.Abstract)
                           && !i.IsStruct)
                .ToList();

            var contextName = GetDbContextName(namespaceName);

            var tmpl = new DbContextTextTemplate(standaloneEntityTypes, contextName, this.mvcProjectName);
            var fileContent = tmpl.TransformText();
            var fileOutputPath = Path.Combine(this.modelsFolderPath, contextName + ".cs");
            File.WriteAllText(fileOutputPath, fileContent);
        }

        private static string GetDbContextName(string namespaceName)
        {
            return namespaceName + "Context";
        }

        private static void PrepareModelsFolder(string path)
        {
            if (Directory.Exists(path))
            {
                ClearFolder(path);
            }

            Directory.CreateDirectory(path);
        }

        private static void ClearFolder(string path)
        {
            var directory = new DirectoryInfo(path);

            foreach (var file in directory.GetFiles().Where(f => !f.Name.Equals("ErrorViewModel")))
            {
                file.Delete();
            }

            foreach (var subDirectory in directory.GetDirectories())
            {
                ClearFolder(subDirectory.FullName);
                subDirectory.Delete();
            }
        }
    }
}
