namespace UMLToMVCConverter
{
    public interface INugetPackageInstaller
    {
        void InstallEntityFrameworkPackage(string mvcProjectCsprojFilePath);
    }
}