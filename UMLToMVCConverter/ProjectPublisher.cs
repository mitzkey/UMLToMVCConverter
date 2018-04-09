namespace UMLToMVCConverter
{
    using System.Diagnostics;
    using System.IO;

    public class ProjectPublisher : IProjectPublisher
    {
        private readonly ILogger logger;

        public ProjectPublisher(ILogger logger)
        {
            this.logger = logger;
        }

        private const string ScriptName = "build_dotnet_project.bat";

        public void PublishProject(string projectFilePath, string outputPath)
        {
            this.logger.LogInfo("Publishing project...");

            var scriptContent = $@"dotnet publish ""{projectFilePath}"" -o ""{outputPath}""";
            
            File.WriteAllText(ScriptName, scriptContent);
            var process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = ScriptName
                }
            };

            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            this.logger.LogInfo(output);
        }
    }
}