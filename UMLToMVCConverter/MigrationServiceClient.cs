﻿namespace UMLToMVCConverter
{
    using System.Diagnostics;
    using System.IO;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Domain;

    public class MigrationServiceClient : IMigrationServiceClient
    {
        private const string AddMigrationScriptName = "add_migration.bat";
        private const string RunMigrationScriptName = "run_migration.bat";
        private const string MigrationsServiceAssemblyPath = @"netcoreapp2.0\MigrationsService.dll";
        private readonly IMvcProject mvcProject;
        private readonly ILogger logger;

        public MigrationServiceClient(IMvcProject mvcProject, ILogger logger)
        {
            this.mvcProject = mvcProject;
            this.logger = logger;
        }

        public void AddMigration()
        {
            var scriptContent = $@"dotnet {MigrationsServiceAssemblyPath} add-migration ""{this.mvcProject.ProjectFolderPath}"" ""{this.mvcProject.DefaultNamespace}"" ""{this.mvcProject.AssemblyPath}""";

            this.logger.LogInfo("Adding migration...");

            this.RunScript(AddMigrationScriptName, scriptContent);
        }

        public void RunMigration()
        {
            var scriptContent = $@"dotnet {MigrationsServiceAssemblyPath} update-database ""{this.mvcProject.ProjectFolderPath}"" ""{this.mvcProject.DefaultNamespace}"" ""{this.mvcProject.AssemblyPath}""";

            this.logger.LogInfo("Running migration...");

            this.RunScript(RunMigrationScriptName, scriptContent);
        }

        private void RunScript(string scriptName, string scriptContent)
        {
            File.WriteAllText(scriptName, scriptContent);
            var process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = scriptName
                }
            };

            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            this.logger.LogInfo(output);
        }
    }
}