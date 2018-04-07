namespace MigrationsService
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 5)
            {
                throw new ArgumentException(
                    "Not enough arguments, specify: <mvcProjectPath> <mvcProjectName> <dbContextName> <migrationsNamespace> <mvcProjectAssemblyPath>");
            }

            var mvcProjectPath = args[0];
            var mvcProjectName = args[1];
            var dbContextName = args[2];
            var migrationsNamespace = args[3];
            var mvcProjectAssemblyPath = args[4];

            var migrationsService = new MigrationsService(mvcProjectPath, mvcProjectName, dbContextName, migrationsNamespace, mvcProjectAssemblyPath);

            migrationsService.AddMigration();
        }
    }
}
