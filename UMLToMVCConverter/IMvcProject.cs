namespace UMLToMVCConverter
{
    public interface IMvcProject
    {
        string CsprojFilePath { get; }
        string ModelsFolderPath { get; }
        string Name { get; }
        string ProjectFolderPath { get; }
        string ViewsFolderPath { get; }
        string DefaultNamespaceName { get; }
        string WorkspaceFolderPath { get; }
        string DbConnectionString { get; }
        string DbContextName { get; }
        string StartupCsPath { get; }
    }
}