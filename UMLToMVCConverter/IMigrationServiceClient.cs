namespace UMLToMVCConverter
{
    public interface IMigrationServiceClient
    {
        void AddMigration(string mvcProjectProjectFolderPath, string mvcProjectDefaultNamespace);
        void RunMigration();
    }
}