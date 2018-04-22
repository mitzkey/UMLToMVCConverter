namespace UMLToMVCConverter.Interfaces
{
    public interface IMigrationServiceClient
    {
        void AddMigration(string mvcProjectProjectFolderPath, string mvcProjectDefaultNamespace);
        void RunMigration();
    }
}