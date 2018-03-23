namespace UMLToMVCConverter
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Design;
    using System.IO;
    using System.Reflection;
    using System.Resources;

    public class MigrationsManager : IMigrationsManager
    {
        private readonly string migrationsNamespace;
        private readonly string mvcProjectName;
        private readonly string dbContextName;
        private readonly string mvcProjectPath;

        public MigrationsManager(string mvcProjectName, string dbContextName, string mvcProjectPath)
        {
            this.mvcProjectName = mvcProjectName;
            this.dbContextName = dbContextName;
            this.migrationsNamespace = Path.Combine(this.mvcProjectName, "Models");
            this.mvcProjectPath = mvcProjectPath;
        }

        public void AddAndRunMigrations(Assembly mvcProjectAssembly)
        {
            this.AddMigration(mvcProjectAssembly);
            this.RunMigration();
        }

        private void RunMigration()
        {
            throw new NotImplementedException();
        }

        private void AddMigration(Assembly mvcProjectAssembly)
        {
            var dbContextFullyQualifiedName = $"{this.mvcProjectName}.Models.{this.dbContextName}";
            var dbContext = mvcProjectAssembly.GetType(dbContextFullyQualifiedName);

            var dbMigrationsConfigurationType = typeof(DbMigrationsConfiguration<>);
            var dbMigrationsConfigurationTypeGeneric = dbMigrationsConfigurationType.MakeGenericType(dbContext);

            var dbMigrationsConfiguration = (DbMigrationsConfiguration)Activator.CreateInstance(dbMigrationsConfigurationTypeGeneric);

            dbMigrationsConfiguration.MigrationsAssembly = mvcProjectAssembly;
            dbMigrationsConfiguration.MigrationsNamespace = this.migrationsNamespace;

            var scaffolder = new MigrationScaffolder(dbMigrationsConfiguration);
            var migration = scaffolder.Scaffold("Migration1");

            this.GenerateMigrationFiles(migration);
        }

        private void GenerateMigrationFiles(ScaffoldedMigration migration)
        {
            var csFilePath = Path.Combine(this.mvcProjectPath, migration.MigrationId + ".cs");
            var designerCsFilePath = Path.Combine(this.mvcProjectPath, migration.MigrationId + ".Designer.cs");
            var resxFilePath = Path.Combine(this.mvcProjectPath, migration.MigrationId + ".resx");

            File.WriteAllText(csFilePath, migration.UserCode);

            File.WriteAllText(designerCsFilePath, migration.DesignerCode);

            using (var writer = new ResXResourceWriter(resxFilePath))
            {
                foreach (var resource in migration.Resources)
                {
                    writer.AddResource(resource.Key, resource.Value);
                }
            }
        }
    }
}