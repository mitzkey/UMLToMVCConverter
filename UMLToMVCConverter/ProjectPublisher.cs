namespace UMLToMVCConverter
{
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Domain;

    public class ProjectPublisher : IProjectPublisher
    {
        private readonly ILogger logger;
        private readonly IScriptRunner scriptRunner;
        private readonly IMvcProject mvcProject;

        public ProjectPublisher(ILogger logger, IScriptRunner scriptRunner, IMvcProject mvcProject)
        {
            this.logger = logger;
            this.scriptRunner = scriptRunner;
            this.mvcProject = mvcProject;
        }

        private const string ScriptName = "build_dotnet_project.bat";

        public void PublishProject()
        {
            this.logger.LogInfo("Publishing project into workspace...");

            var scriptContent = $@"dotnet publish ""{this.mvcProject.CsprojFilePath}"" -o ""{this.mvcProject.WorkspaceFolderPath}""";

            this.scriptRunner.Run(ScriptName, scriptContent);
        }
    }
}