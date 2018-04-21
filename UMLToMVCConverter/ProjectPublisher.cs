namespace UMLToMVCConverter
{
    public class ProjectPublisher : IProjectPublisher
    {
        private readonly ILogger logger;
        private readonly IScriptRunner scriptRunner;

        public ProjectPublisher(ILogger logger, IScriptRunner scriptRunner)
        {
            this.logger = logger;
            this.scriptRunner = scriptRunner;
        }

        private const string ScriptName = "build_dotnet_project.bat";

        public void PublishProject(string projectFilePath, string outputPath)
        {
            this.logger.LogInfo("Publishing project into workspace...");

            var scriptContent = $@"dotnet publish ""{projectFilePath}"" -o ""{outputPath}""";

            this.scriptRunner.Run(ScriptName, scriptContent);
        }
    }
}