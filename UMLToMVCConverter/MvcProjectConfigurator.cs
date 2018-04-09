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
        private readonly IMvcProject mvcProject;
        private readonly IStartupCsConfigurator startupCsConfigurator;
        private readonly IProjectPublisher projectPublisher;

        public MvcProjectConfigurator(
            IMvcProject mvcProject,
            IStartupCsConfigurator startupCsConfigurator,
            IProjectPublisher projectPublisher)
        {
            this.mvcProject = mvcProject;
            this.startupCsConfigurator = startupCsConfigurator;
            this.projectPublisher = projectPublisher;
        }

        public void SetUpMvcProject(List<CodeTypeDeclaration> codeTypeDeclarations)
        {
            this.SetUpAppsettingsDbConnection(this.mvcProject.DbContextName);

            this.startupCsConfigurator.SetUpStartupCsDbContextUse(this.mvcProject.DbContextName);

            ClearFolder(this.mvcProject.ViewsFolderPath);
            PrepareFolder(this.mvcProject.ModelsFolderPath);
            this.GenerateModels(codeTypeDeclarations);
            this.GenerateDbContextClass(codeTypeDeclarations, this.mvcProject.DbContextName);

            this.projectPublisher.PublishProject(this.mvcProject.CsprojFilePath, this.mvcProject.WorkspaceFolderPath);

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
                [contextName] = this.mvcProject.DbConnectionString
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

            var tmpl = new DbContextTextTemplate(standaloneEntityTypes, this.mvcProject.DbContextName, this.mvcProject.Name);
            var fileContent = tmpl.TransformText();
            var fileOutputPath = Path.Combine(this.mvcProject.ModelsFolderPath, this.mvcProject.DbContextName + ".cs");
            File.WriteAllText(fileOutputPath, fileContent);
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
