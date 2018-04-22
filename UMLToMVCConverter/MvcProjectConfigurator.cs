﻿namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using UMLToMVCConverter.CodeTemplates;
    using UMLToMVCConverter.ExtendedTypes;
    using UMLToMVCConverter.ExtensionMethods;

    public class MvcProjectConfigurator : IMvcProjectConfigurator
    {
        private readonly IMvcProject mvcProject;
        private readonly IStartupCsConfigurator startupCsConfigurator;
        private readonly IProjectPublisher projectPublisher;
        private readonly IMigrationServiceClient migrationsServiceClient;
        private readonly ILogger logger;
        private readonly IMigrationsManagerClassTextTemplate migrationsManagerClassTextTemplate;
        private readonly IDbContextFactoryClassTextTemplate dbContextFactoryClassTextTemplate;
        private readonly IDbContextClassTextTemplate dbContextClassTextTemplate;
        private readonly IComponentContext componentContext;

        public MvcProjectConfigurator(
            IMvcProject mvcProject,
            IStartupCsConfigurator startupCsConfigurator,
            IProjectPublisher projectPublisher,
            IMigrationServiceClient migrationsServiceClient,
            ILogger logger,
            IMigrationsManagerClassTextTemplate migrationsManagerClassTextTemplate,
            IDbContextFactoryClassTextTemplate dbContextFactoryClassTextTemplate,
            IDbContextClassTextTemplate dbContextClassTextTemplate,
            IComponentContext componentContext)
        {
            this.mvcProject = mvcProject;
            this.startupCsConfigurator = startupCsConfigurator;
            this.projectPublisher = projectPublisher;
            this.migrationsServiceClient = migrationsServiceClient;
            this.logger = logger;
            this.migrationsManagerClassTextTemplate = migrationsManagerClassTextTemplate;
            this.dbContextFactoryClassTextTemplate = dbContextFactoryClassTextTemplate;
            this.dbContextClassTextTemplate = dbContextClassTextTemplate;
            this.componentContext = componentContext;
        }

        public void SetUpMvcProject(IEnumerable<ExtendedCodeTypeDeclaration> codeTypeDeclarations)
        {
            this.SetUpAppsettingsDbConnection();

            this.startupCsConfigurator.SetUpStartupCsDbContextUse(this.mvcProject.DbContextName);

            ClearFolder(this.mvcProject.ControllersFolderPath);
            PrepareFolder(this.mvcProject.ModelsFolderPath);

            this.GenerateModels(codeTypeDeclarations);
            this.GenerateDbContextClass(codeTypeDeclarations);
            this.GenerateMigrationsManager();
            this.GenerateDbContextFactoryClass();
            Directory.CreateDirectory(this.mvcProject.MigrationsFolderPath);

            this.projectPublisher.PublishProject(this.mvcProject.CsprojFilePath, this.mvcProject.WorkspaceFolderPath);

            this.migrationsServiceClient.AddMigration(
                this.mvcProject.ProjectFolderPath,
                this.mvcProject.DefaultNamespace);

            this.projectPublisher.PublishProject(this.mvcProject.CsprojFilePath, this.mvcProject.WorkspaceFolderPath);

            this.migrationsServiceClient.RunMigration();
        }

        private void GenerateDbContextFactoryClass()
        {
            this.logger.LogInfo("Generating DesignTimeDbContextFactory.cs");

            var fileContent = this.dbContextFactoryClassTextTemplate.TransformText();
            var fileOutputPath = Path.Combine(this.mvcProject.ModelsFolderPath, "DesignTimeDbContextFactory.cs");
            File.WriteAllText(fileOutputPath, fileContent);

            this.logger.LogInfo($"Generated {fileOutputPath}");
        }

        private void GenerateMigrationsManager()
        {
            this.logger.LogInfo("Generating MigrationsManager.cs");

            var fileContent = this.migrationsManagerClassTextTemplate.TransformText();
            var fileOutputPath = Path.Combine(this.mvcProject.ProjectFolderPath, "MigrationsManager.cs");
            File.WriteAllText(fileOutputPath, fileContent);

            this.logger.LogInfo($"Generated {fileOutputPath}");
        }

        private void SetUpAppsettingsDbConnection()
        {
            this.logger.LogInfo("Setting up appsettings.json db connection...");

            var appsettingsJsonPath = Path.Combine(this.mvcProject.ProjectFolderPath, "appsettings.json");
            var appsettingsJsonContent = File.ReadAllText(appsettingsJsonPath);
            var appsettingsJson = JObject.Parse(appsettingsJsonContent);

            var connectionStringConfig = new JObject
            {
                [this.mvcProject.DbContextName] = this.mvcProject.DbConnectionString
            };

            var connectionStrings = new JProperty("ConnectionStrings", connectionStringConfig);

            appsettingsJson.AddOrUpdate(connectionStrings);

            var appsettingsJsonOutput = JsonConvert.SerializeObject(appsettingsJson, Formatting.Indented);

            File.WriteAllText(appsettingsJsonPath, appsettingsJsonOutput);

            this.logger.LogInfo($"Generated: {appsettingsJsonPath}");

            var appsettingsJsonWorkingDirPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            File.WriteAllText(appsettingsJsonWorkingDirPath, appsettingsJsonOutput);

            this.logger.LogInfo($"Generated: {appsettingsJsonWorkingDirPath}");
        }

        private void GenerateModels(IEnumerable<ExtendedCodeTypeDeclaration> codeTypeDeclarations)
        {
            this.logger.LogInfo("Generating models...");

            var codeTypeDeclarationsList = codeTypeDeclarations.ToList();
            foreach (var codeTypeDeclaration in codeTypeDeclarationsList)
            {
                var modelClassTextTemplate = this.componentContext.Resolve<IModelClassTextTemplate>();
                var fileContent = modelClassTextTemplate.TransformText(codeTypeDeclaration);
                var fileOutputPath = Path.Combine(this.mvcProject.ModelsFolderPath, codeTypeDeclaration.Name + ".cs");
                File.WriteAllText(fileOutputPath, fileContent);

                this.logger.LogInfo($"Generated {fileOutputPath}");
            }
        }

        private void GenerateDbContextClass(IEnumerable<ExtendedCodeTypeDeclaration> codeTypeDeclarations)
        {
            this.logger.LogInfo("Generating db context class...");

            var standaloneEntityTypes = codeTypeDeclarations.
                Where(i => !i.TypeAttributes.HasFlag(TypeAttributes.Abstract)
                           && !i.IsStruct)
                .ToList();

            var fileContent = this.dbContextClassTextTemplate.TransformText(standaloneEntityTypes);
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
            if (!Directory.Exists(path))
            {
                return;
            }

            var directory = new DirectoryInfo(path);

            foreach (var file in directory.GetFiles().Where(f => !f.Name.Equals("ErrorViewModel.cs")))
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
