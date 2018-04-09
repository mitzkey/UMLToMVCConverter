namespace UMLToMVCConverter
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public class ProjectPublisher : IProjectPublisher
    {
        private const string ScriptName = "build_dotnet_project.bat";

        public void PublishProject(string projectFilePath, string outputPath)
        {
            var scriptContent = $@"dotnet publish ""{projectFilePath}"" -o ""{outputPath}""";
            
            File.WriteAllText(ScriptName, scriptContent);
            // Start the child process.
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

            Console.WriteLine(output);
        }
    }
}