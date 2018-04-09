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
        private readonly IMigrationServiceClient migrationsServiceClient;
        private readonly ILogger logger;
        private readonly IMigrationsManagerClassTextTemplate migrationsManagerClassTextTemplate;

        public MvcProjectConfigurator(
            IMvcProject mvcProject,
            IStartupCsConfigurator startupCsConfigurator,
            IProjectPublisher projectPublisher,
            IMigrationServiceClient migrationsServiceClient,
            ILogger logger,
            IMigrationsManagerClassTextTemplate migrationsManagerClassTextTemplate)
        {
            this.mvcProject = mvcProject;
            this.startupCsConfigurator = startupCsConfigurator;
            this.projectPublisher = projectPublisher;
            this.migrationsServiceClient = migrationsServiceClient;
            this.logger = logger;
            this.migrationsManagerClassTextTemplate = migrationsManagerClassTextTemplate;
        }

        public void SetUpMvcProject(List<CodeTypeDeclaration> codeTypeDeclarations)
        {
            this.SetUpAppsettingsDbConnection(this.mvcProject.DbContextName);

            this.startupCsConfigurator.SetUpStartupCsDbContextUse(this.mvcProject.DbContextName);

            ClearFolder(this.mvcProject.ViewsFolderPath);
            PrepareFolder(this.mvcProject.ModelsFolderPath);

            this.GenerateModels(codeTypeDeclarations);
            this.GenerateDbContextClass(codeTypeDeclarations);
            this.GenerateMigrationsManager();

            this.projectPublisher.PublishProject(this.mvcProject.CsprojFilePath, this.mvcProject.WorkspaceFolderPath);

            this.migrationsServiceClient.AddMigration(
                this.mvcProject.ProjectFolderPath,
                this.mvcProject.DefaultNamespace);

            this.projectPublisher.PublishProject(this.mvcProject.CsprojFilePath, this.mvcProject.WorkspaceFolderPath);

            this.migrationsServiceClient.RunMigration();
        }

        private void GenerateMigrationsManager()
        {
            this.logger.LogInfo("Generating MigrationsManager.cs");

            var fileContent = this.migrationsManagerClassTextTemplate.TransformText();
            var fileOutputPath = Path.Combine(this.mvcProject.ProjectFolderPath, "MigrationsManager.cs");
            File.WriteAllText(fileOutputPath, fileContent);

            this.logger.LogInfo($"Generated {fileOutputPath}");
        }

        private void SetUpAppsettingsDbConnection(string contextName)
        {
            this.logger.LogInfo("Setting up appsettings.json db connection...");

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
            this.logger.LogInfo("Generating models...");

            var codeTypeDeclarationsList = codeTypeDeclarations.ToList();
            foreach (var ctd in codeTypeDeclarationsList)
            {
                var template = new ModelClassTextTemplate(ctd, this.mvcProject.Name);
                var fileContent = template.TransformText();
                var fileOutputPath = Path.Combine(this.mvcProject.ModelsFolderPath, ctd.Name + ".cs");
                File.WriteAllText(fileOutputPath, fileContent);

                this.logger.LogInfo($"Generated {fileOutputPath}");
            }
        }

        private void GenerateDbContextClass(List<CodeTypeDeclaration> codeTypeDeclarations)
        {
            this.logger.LogInfo("Generating db context class...");

            var standaloneEntityTypes = codeTypeDeclarations.
                Where(i => !i.TypeAttributes.HasFlag(TypeAttributes.Abstract)
                           && !i.IsStruct)
                .ToList();

            var template = new DbContextTextTemplate(standaloneEntityTypes, this.mvcProject.DbContextName, this.mvcProject.Name);
            var fileContent = template.TransformText();
            var fileOutputPath = Path.Combine(this.mvcProject.ModelsFolderPath, this.mvcProject.DbContextName + ".cs");
            File.WriteAllText(fileOutputPath, fileContent);

            this.logger.LogInfo($"Generated {fileOutputPath}");
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
