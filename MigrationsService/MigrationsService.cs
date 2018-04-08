namespace MigrationsService
{
    using System;
    using System.Reflection;

    public class MigrationsService
    {
        private const string MigrationsManagerTypeName = "WebApplication1.MigrationsManager";
        private const string AddMigrationMethodName = "AddMigration";
        private const string RunMigrationMethodName = "RunMigration";
        private readonly Type migrationsManagerType;
        private readonly object migrationsManager;

        public MigrationsService(string mvcProjectAssemblyPath)
        {
            var mvcProjectAssembly = Assembly.LoadFrom(mvcProjectAssemblyPath);
            this.migrationsManagerType = mvcProjectAssembly.GetType(MigrationsManagerTypeName);
            this.migrationsManager = Activator.CreateInstance(migrationsManagerType);
        }

        public string AddMigration(string mvcProjectFolderPath, string migrationsNamespace)
        {
            var addMigrationMethodInfo = migrationsManagerType.GetMethod(AddMigrationMethodName);

            return addMigrationMethodInfo.Invoke(this.migrationsManager,
                new object[] { mvcProjectFolderPath, migrationsNamespace }).ToString();
        }

        public void RunMigration()
        {
            var runMigrationMethodInfo = migrationsManagerType.GetMethod(RunMigrationMethodName);

            runMigrationMethodInfo.Invoke(this.migrationsManager, null);
        }
    }
}
