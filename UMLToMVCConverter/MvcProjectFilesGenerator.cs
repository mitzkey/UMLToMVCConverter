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
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Domain.Models;

    public class MvcProjectFilesGenerator : IMvcProjectFilesGenerator
    {
        private readonly ILogger logger;
        private readonly IStartupCsConfigurator startupCsConfigurator;
        private readonly MvcProject mvcProject;
        private readonly IComponentContext componentContext;
        private readonly IDbContextClassTextTemplate dbContextClassTextTemplate;
        private readonly IMigrationsManagerClassTextTemplate migrationsManagerClassTextTemplate;
        private readonly IDbContextFactoryClassTextTemplate dbContextFactoryClassTextTemplate;
        private readonly IDatabaseSeedInitializerTextTemplate databaseSeedInitializerTextTemplate;
        private readonly IProgramCsTextTemplate programCsTextTemplate;

        public MvcProjectFilesGenerator(ILogger logger, IStartupCsConfigurator startupCsConfigurator, MvcProject mvcProject, IComponentContext componentContext, IDbContextClassTextTemplate dbContextClassTextTemplate, IMigrationsManagerClassTextTemplate migrationsManagerClassTextTemplate, IDbContextFactoryClassTextTemplate dbContextFactoryClassTextTemplate, IDatabaseSeedInitializerTextTemplate databaseSeedInitializerTextTemplate, IProgramCsTextTemplate programCsTextTemplate)
        {
            this.logger = logger;
            this.startupCsConfigurator = startupCsConfigurator;
            this.mvcProject = mvcProject;
            this.componentContext = componentContext;
            this.dbContextClassTextTemplate = dbContextClassTextTemplate;
            this.migrationsManagerClassTextTemplate = migrationsManagerClassTextTemplate;
            this.dbContextFactoryClassTextTemplate = dbContextFactoryClassTextTemplate;
            this.databaseSeedInitializerTextTemplate = databaseSeedInitializerTextTemplate;
            this.programCsTextTemplate = programCsTextTemplate;
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
            this.GenerateDatabaseSeedInitializer(dataModel.EnumerationModels);
            this.GenerateProgramCs();
            Directory.CreateDirectory(this.mvcProject.MigrationsFolderPath);
        }

        private void GenerateProgramCs()
        {
            this.logger.LogInfo("Generating Program.cs");

            var fileContent = this.programCsTextTemplate.TransformText();
            var fileOutputPath = Path.Combine(this.mvcProject.ProjectFolderPath, "Program.cs");
            File.WriteAllText(fileOutputPath, fileContent);

            this.logger.LogInfo($"Generated {fileOutputPath}");
        }

        private void GenerateDatabaseSeedInitializer(IEnumerable<Enumeration> enumerationModels)
        {
            this.logger.LogInfo("Generating DatabaseSeedInitializer.cs");

            var fileContent = this.databaseSeedInitializerTextTemplate.TransformText(enumerationModels);
            var fileOutputPath = Path.Combine(this.mvcProject.ProjectFolderPath, "DatabaseSeedInitializer.cs");
            File.WriteAllText(fileOutputPath, fileContent);

            this.logger.LogInfo($"Generated {fileOutputPath}");
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


        private void GenerateDbContextClass(IEnumerable<ExtendedCodeTypeDeclaration> codeTypeDeclarations, IEnumerable<EFRelationship> relationshipModels)
        {
            this.logger.LogInfo("Generating db context class...");

            var standaloneEntityTypes = codeTypeDeclarations.
                Where(i => !i.TypeAttributes.HasFlag(TypeAttributes.Abstract)
                           && !i.IsStruct)
                .ToList();

            var structs = codeTypeDeclarations.Where(x => x.IsStruct);

            var fileContent = this.dbContextClassTextTemplate.TransformText(standaloneEntityTypes, relationshipModels, structs);
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