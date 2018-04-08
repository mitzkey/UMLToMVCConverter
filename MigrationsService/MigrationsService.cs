namespace MigrationsService
{
    using System;
    using System.Reflection;

    public class MigrationsService
    {
        private const string MigrationsManagerTypeName = "WebApplication1.MigrationsManager";
        private const string AddMigrationMethodName = "AddMigration";
        private const string RunMigrationMethodName = "RunMigration";
        private readonly string migrationsNamespace;
        private readonly string mvcProjectFolderPath;
        private readonly Assembly mvcProjectAssembly;

        public MigrationsService(string mvcProjectFolderPath, string migrationsNamespace, string mvcProjectAssemblyPath)
        {
            this.migrationsNamespace = migrationsNamespace;
            this.mvcProjectFolderPath = mvcProjectFolderPath;
            this.mvcProjectAssembly = Assembly.LoadFrom(mvcProjectAssemblyPath);
        }

        public string AddMigration()
        {
            var migrationsManagerType = this.mvcProjectAssembly.GetType(MigrationsManagerTypeName);
            var migrationsManager = Activator.CreateInstance(
                migrationsManagerType,
                new object[] { this.mvcProjectFolderPath, this.migrationsNamespace});
            var addMigrationMethodInfo = migrationsManagerType.GetMethod(AddMigrationMethodName);

            return addMigrationMethodInfo.Invoke(migrationsManager, null).ToString();
        }

        public void RunMigration()
        {
        }
    }
}
