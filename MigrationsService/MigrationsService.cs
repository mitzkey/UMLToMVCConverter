namespace MigrationsService
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.EntityFrameworkCore.Design.Internal;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Microsoft.EntityFrameworkCore.Migrations.Design;
    using Microsoft.EntityFrameworkCore.Migrations.Internal;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.Extensions.DependencyInjection;

    public class MigrationsService
    {
        private readonly string mvcProjectName;
        private readonly string dbContextName;
        private readonly string migrationsNamespace;
        private readonly string mvcProjectPath;
        private readonly Assembly mvcProjectAssembly;

        public MigrationsService(string mvcProjectPath, string mvcProjectName, string dbContextName, string migrationsNamespace, string mvcProjectAssemblyPath)
        {
            this.mvcProjectName = mvcProjectName;
            this.dbContextName = dbContextName;
            this.migrationsNamespace = migrationsNamespace;
            this.mvcProjectPath = mvcProjectPath;
            this.mvcProjectAssembly = Assembly.LoadFile(mvcProjectAssemblyPath);
        }

        public string AddMigration()
        {
            var dbContextFullyQualifiedName = $"{this.mvcProjectName}.Models.{this.dbContextName}";
            var dbContext = this.mvcProjectAssembly.GetType(dbContextFullyQualifiedName);

            var dbContextOptionsType = typeof(DbContextOptions<>);
            var dbContextOptionsGenericType = dbContextOptionsType.MakeGenericType(dbContext);
            var dbContextOptions = Activator.CreateInstance(dbContextOptionsGenericType);
            

            using (var db = (DbContext)Activator.CreateInstance(dbContext, dbContextOptions))
            {
                var reporter = new OperationReporter(
                    new OperationReportHandler(
                        m => Console.WriteLine("  error: " + m),
                        m => Console.WriteLine("   warn: " + m),
                        m => Console.WriteLine("   info: " + m),
                        m => Console.WriteLine("verbose: " + m)));

                var designTimeServices = new ServiceCollection()
                    .AddSingleton(db.GetService<IHistoryRepository>())
                    .AddSingleton(db.GetService<IMigrationsIdGenerator>())
                    .AddSingleton(db.GetService<IMigrationsModelDiffer>())
                    .AddSingleton(db.GetService<IMigrationsAssembly>())
                    .AddSingleton(db.Model)
                    .AddSingleton(db.GetService<ICurrentDbContext>())
                    .AddSingleton(db.GetService<IDatabaseProvider>())
                    .AddSingleton<MigrationsCodeGeneratorDependencies>()
                    .AddSingleton<ICSharpHelper, CSharpHelper>()
                    .AddSingleton<CSharpMigrationOperationGeneratorDependencies>()
                    .AddSingleton<ICSharpMigrationOperationGenerator, CSharpMigrationOperationGenerator>()
                    .AddSingleton<CSharpSnapshotGeneratorDependencies>()
                    .AddSingleton<ICSharpSnapshotGenerator, CSharpSnapshotGenerator>()
                    .AddSingleton<CSharpMigrationsGeneratorDependencies>()
                    .AddSingleton<IMigrationsCodeGenerator, CSharpMigrationsGenerator>()
                    .AddSingleton<IOperationReporter>(reporter)
                    .AddSingleton<MigrationsScaffolderDependencies>()
                    .AddSingleton<MigrationsScaffolder>()
                    .AddSingleton<ISnapshotModelProcessor, SnapshotModelProcessor>()
                    .BuildServiceProvider();

                var scaffolder = designTimeServices.GetRequiredService<MigrationsScaffolder>();

                var migration = scaffolder.ScaffoldMigration(
                    "UMLToMVCConverterMigration_" + Guid.NewGuid(),
                    this.migrationsNamespace);

                var migrationFolder = Path.Combine(this.mvcProjectPath, "Migrations");

                var migrationCodeFilePath = Path.Combine(
                    migrationFolder,
                    migration.MigrationId + migration.FileExtension);

                var migrationMetadataCodeFilePath = Path.Combine(
                    migrationFolder,
                    migration.MigrationId + ".Designer" + migration.FileExtension);

                var migrationSnapshotFilePath = Path.Combine(
                    migrationFolder,
                    migration.SnapshotName + migration.FileExtension);

                File.WriteAllText(
                    migrationCodeFilePath,
                    migration.MigrationCode);
                File.WriteAllText(
                    migrationMetadataCodeFilePath,
                    migration.MetadataCode);
                File.WriteAllText(
                    migrationSnapshotFilePath,
                    migration.SnapshotCode);

                return migrationCodeFilePath;
            }
        }

        public void RunMigration()
        {
            /*
            using (var db = new MyDbContext())
            {
                db.Database.Migrate();
            }
            */
            throw new NotImplementedException();
        }
    }
}
