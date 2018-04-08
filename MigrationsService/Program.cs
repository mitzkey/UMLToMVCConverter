namespace MigrationsService
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                throw new ArgumentException(
                    "Not enough arguments, specify: <mvcProjectFolderPath> <migrationsNamespace> <mvcProjectAssemblyPath>");
            }

            var mvcProjectFolderPath = args[0];
            var migrationsNamespace = args[1];
            var mvcProjectAssemblyPath = args[2];

            var migrationsService = new MigrationsService(mvcProjectAssemblyPath);

            migrationsService.AddMigration(mvcProjectFolderPath, migrationsNamespace);
        }
    }
}
