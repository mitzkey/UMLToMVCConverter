namespace UMLToMVCConverter
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
    using UMLToMVCConverter.Interfaces;

    public class MvcProjectFilesGenerator : IMvcProjectFilesGenerator
    {
        private readonly ILogger logger;
        private readonly IStartupCsConfigurator startupCsConfigurator;
        private readonly IMvcProject mvcProject;
        private readonly IComponentContext componentContext;
        private readonly IDbContextClassTextTemplate dbContextClassTextTemplate;
        private readonly IMigrationsManagerClassTextTemplate migrationsManagerClassTextTemplate;
        private readonly IDbContextFactoryClassTextTemplate dbContextFactoryClassTextTemplate;

        public MvcProjectFilesGenerator(ILogger logger, IStartupCsConfigurator startupCsConfigurator, IMvcProject mvcProject, IComponentContext componentContext, IDbContextClassTextTemplate dbContextClassTextTemplate, IMigrationsManagerClassTextTemplate migrationsManagerClassTextTemplate, IDbContextFactoryClassTextTemplate dbContextFactoryClassTextTemplate)
        {
            this.logger = logger;
            this.startupCsConfigurator = startupCsConfigurator;
            this.mvcProject = mvcProject;
            this.componentContext = componentContext;
            this.dbContextClassTextTemplate = dbContextClassTextTemplate;
            this.migrationsManagerClassTextTemplate = migrationsManagerClassTextTemplate;
            this.dbContextFactoryClassTextTemplate = dbContextFactoryClassTextTemplate;
        }

        public void GenerateFiles(DataModel dataModel)
        {
            this.SetUpAppsettingsDbConnection();

            this.startupCsConfigurator.SetUpStartupCsDbContextUse(this.mvcProject.DbContextName);

            ClearFolder(this.mvcProject.ControllersFolderPath);
            PrepareFolder(this.mvcProject.ModelsFolderPath);

            this.GenerateModels(dataModel.Types);
            this.GenerateDbContextClass(dataModel.Types, dataModel.EFRelationshipModels);
            this.GenerateMigrationsManager();
            this.GenerateDbContextFactoryClass();
            Directory.CreateDirectory(this.mvcProject.MigrationsFolderPath);
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


        private void GenerateDbContextClass(IEnumerable<ExtendedCodeTypeDeclaration> codeTypeDeclarations, IEnumerable<EFRelationshipModel> relationshipModels)
        {
            this.logger.LogInfo("Generating db context class...");

            var standaloneEntityTypes = codeTypeDeclarations.
                Where(i => !i.TypeAttributes.HasFlag(TypeAttributes.Abstract)
                           && !i.IsStruct)
                .ToList();

            var fileContent = this.dbContextClassTextTemplate.TransformText(standaloneEntityTypes, relationshipModels);
            var fileOutputPath = Path.Combine(this.mvcProject.ModelsFolderPath, this.mvcProject.DbContextName + ".cs");
            File.WriteAllText(fileOutputPath, fileContent);

            this.logger.LogInfo($"Generated {fileOutputPath}");
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
    }
}