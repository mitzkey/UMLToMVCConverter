namespace UMLToMVCConverter
{
    public class NugetPackageInstaller : INugetPackageInstaller
    {
        private readonly IScriptRunner scriptRunner;
        private string ScriptName = "install_nuget_packages.bat";

        public NugetPackageInstaller(IScriptRunner scriptRunner)
        {
            this.scriptRunner = scriptRunner;
        }

        public void InstallEntityFrameworkPackage(string mvcProjectCsprojFilePath)
        {
            string scriptContent = "echo test";
            this.scriptRunner.Run(ScriptName, scriptContent);
        }
    }
}