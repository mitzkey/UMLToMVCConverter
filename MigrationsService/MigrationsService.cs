namespace MigrationsService
{
    using System;
    using System.Reflection;

    public class MigrationsService
    {
        private const string MigrationsManagerTypeName = "MigrationsManager";
        private const string AddMigrationMethodName = "AddMigration";
        private const string RunMigrationMethodName = "RunMigration";
        private readonly Type migrationsManagerType;
        private readonly object migrationsManager;
        private readonly string migrationsNamespace;

        public MigrationsService(string mvcProjectAssemblyPath, string migrationsNamespace)
        {
            var mvcProjectAssembly = Assembly.LoadFrom(mvcProjectAssemblyPath);
            this.migrationsNamespace = migrationsNamespace;
            this.migrationsManagerType = mvcProjectAssembly.GetType($"{this.migrationsNamespace}.{MigrationsManagerTypeName}");
            this.migrationsManager = Activator.CreateInstance(this.migrationsManagerType);
        }

        public string AddMigration(string mvcProjectFolderPath)
        {
            var addMigrationMethodInfo = this.migrationsManagerType.GetMethod(AddMigrationMethodName);

            return addMigrationMethodInfo.Invoke(this.migrationsManager,
                new object[] { mvcProjectFolderPath, this.migrationsNamespace }).ToString();
        }

        public void RunMigration()
        {
            var runMigrationMethodInfo = this.migrationsManagerType.GetMethod(RunMigrationMethodName);

            runMigrationMethodInfo.Invoke(this.migrationsManager, null);
        }
    }
}
