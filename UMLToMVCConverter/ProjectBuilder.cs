namespace UMLToMVCConverter
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public class ProjectBuilder : IProjectBuilder
    {
        private const string ScriptName = "build_dotnet_project.bat";

        public void BuildProject(string projectFilePath, string outputPath)
        {
            File.WriteAllText(ScriptName, $@"dotnet msbuild {projectFilePath} /p:OutputPath=""{outputPath}""");
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