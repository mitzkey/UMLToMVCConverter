namespace UMLToEFConverter
{
    using System.Diagnostics;
    using System.IO;
    using UMLToEFConverter.Common;
    using UMLToEFConverter.Interfaces;
    using UMLToEFConverter.Models;

    public class MigrationServiceClient : IMigrationServiceClient
    {
        private const string AddMigrationScriptName = "add_migration.bat";
        private const string RunMigrationScriptName = "run_migration.bat";
        private const string MigrationsServiceAssemblyPath = @"netcoreapp2.0\MigrationsService.dll";
        private readonly MvcProject mvcProject;
        private readonly ILogger logger;
        private readonly IScriptRunner scriptRunner;

        public MigrationServiceClient(MvcProject mvcProject, ILogger logger, IScriptRunner scriptRunner)
        {
            this.mvcProject = mvcProject;
            this.logger = logger;
            this.scriptRunner = scriptRunner;
        }

        public void AddMigration()
        {
            var scriptContent = $@"dotnet {MigrationsServiceAssemblyPath} add-migration ""{this.mvcProject.ProjectFolderPath}"" ""{this.mvcProject.DefaultNamespace}"" ""{this.mvcProject.AssemblyPath}""";

            this.logger.LogInfo("Adding migration...");

            this.scriptRunner.Run(AddMigrationScriptName, scriptContent);
        }

        public void RunMigration()
        {
            var scriptContent = $@"dotnet {MigrationsServiceAssemblyPath} update-database ""{this.mvcProject.ProjectFolderPath}"" ""{this.mvcProject.DefaultNamespace}"" ""{this.mvcProject.AssemblyPath}""";

            this.logger.LogInfo("Running migration...");

            this.scriptRunner.Run(RunMigrationScriptName, scriptContent);
        }
    }
}