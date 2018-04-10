namespace MigrationsService
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                throw new ArgumentException(
                    "Not enough arguments, specify: <command> <mvcProjectFolderPath> <migrationsNamespace> <mvcProjectAssemblyPath>" +
                    "\ncommands: add-migration, update-database");
            }

            var command = args[0];
            var mvcProjectFolderPath = args[1];
            var migrationsNamespace = args[2];
            var mvcProjectAssemblyPath = args[3];

            var migrationsService = new MigrationsService(mvcProjectAssemblyPath, migrationsNamespace);

            switch (command)
            {
                case "add-migration":
                    var migrationAdded = migrationsService.AddMigration(mvcProjectFolderPath);
                    Console.WriteLine("Successfully added migration:\n" + migrationAdded);
                    break;
                case "update-database":
                    migrationsService.RunMigration();
                    Console.WriteLine("Successfully updated database");
                    break;
                default:
                    throw new ArgumentException("Wrong command, either 'run-migration' or 'update-database' enabled");
            }
        }
    }
}
