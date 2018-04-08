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
        private readonly MvcProject mvcProject;
        private readonly string connectionString;
        private readonly string mvcProjectPublishOutputFolder;
        private readonly IStartupCsConfigurator startupCsConfigurator;
        private readonly IProjectBuilder projectBuilder;

        public MvcProjectConfigurator(MvcProject mvcProject, string connectionString, string mvcProjectPublishOutputFolder)
        {
            Insist.IsNotNullOrWhiteSpace(connectionString, nameof(connectionString));
            this.connectionString = connectionString;
            this.mvcProjectPublishOutputFolder = mvcProjectPublishOutputFolder;
            this.mvcProject = mvcProject;

            this.startupCsConfigurator = new StartupCsConfigurator(this.mvcProject.ProjectFolderPath);
            this.projectBuilder = new ProjectBuilder();
        }

        public void SetUpMvcProject(List<CodeTypeDeclaration> codeTypeDeclarations, string namespaceName)
        {
            Insist.IsNotNullOrWhiteSpace(namespaceName, nameof(namespaceName));

            var dbContextName = GetDbContextName(namespaceName);

            this.SetUpAppsettingsDbConnection(dbContextName);

            this.startupCsConfigurator.SetUpStartupCsDbContextUse(dbContextName);

            PrepareFolder(this.mvcProject.ModelsFolderPath);
            this.GenerateModels(codeTypeDeclarations);
            this.GenerateDbContextClass(codeTypeDeclarations, namespaceName);

            ClearFolder(this.mvcProject.ViewsFolderPath);
            this.projectBuilder.BuildProject(this.mvcProject.CsprojFilePath, this.mvcProjectPublishOutputFolder);

            // this.migrationsManager = new MigrationsManager(this.mvcProjectName, dbContextName, this.mvcProjectFolderPath);
            // this.migrationsManager.AddAndRunMigrations(mvcProjectAssembly);
        }

        private void SetUpAppsettingsDbConnection(string contextName)
        {
            var appsettingJsonPath = Path.Combine(this.mvcProject.ProjectFolderPath, "appsettings.json");
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
                var tmpl = new ModelClassTextTemplate(ctd, this.mvcProject.Name);
                var fileContent = tmpl.TransformText();
                var filesOutputPath = Path.Combine(this.mvcProject.ModelsFolderPath, ctd.Name + ".cs");
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

            var tmpl = new DbContextTextTemplate(standaloneEntityTypes, contextName, this.mvcProject.Name);
            var fileContent = tmpl.TransformText();
            var fileOutputPath = Path.Combine(this.mvcProject.ModelsFolderPath, contextName + ".cs");
            File.WriteAllText(fileOutputPath, fileContent);
        }

        private static string GetDbContextName(string namespaceName)
        {
            return namespaceName + "Context";
        }

        private static void PrepareFolder(string path)
        {
            if (Directory.Exists(path))
            {
                ClearFolder(path);
            }

            Directory.CreateDirectory(path);
        }

        private static void ClearFolder(string path)
        {
            if (Directory.Exists(path))
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
}
