namespace WebApplication2
{
    using System;
    using System.IO;
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
    using WebApplication2.Models;

    public class MigrationsManager
    {
        public string AddMigration(string mvcProjectFolderPath, string migrationsNamespace)
        {
            using (var db = DbContextActivator.CreateInstance(typeof(TestowyZKartki01Context)))
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
                    migrationsNamespace);

                var migrationFolder = Path.Combine(mvcProjectFolderPath, "Migrations");

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
            using (var db = DbContextActivator.CreateInstance(typeof(TestowyZKartki01Context)))
            {
                db.Database.Migrate();
            }
        }
    }
}
