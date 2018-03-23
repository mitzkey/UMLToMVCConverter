namespace UMLToMVCConverter
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Build.Evaluation;

    public class ProjectBuilder : IProjectBuilder
    {
        public Assembly BuildProject(string projectFolderPath)
        {
            var projectName = "WebApplication1";

            var projectPath = Path.Combine(projectFolderPath, projectName + ".csproj");

            var project = new Project(projectPath);

            RunBuildScript(projectPath);

            var outputPath = Path.Combine(projectFolderPath, project.Properties.Single(p => p.Name == "OutputPath").EvaluatedValue);

            var assemblyPath = Path.Combine(outputPath, projectName + ".dll");

            return Assembly.LoadFrom(assemblyPath);
        }

        private static void RunBuildScript(string projectPath)
        {
            var scriptName = "run.bat";

            File.WriteAllText(scriptName, $@"dotnet msbuild {projectPath}");
            // Start the child process.
            var process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = scriptName
                }
            };

            // Redirect the output stream of the child process.
            process.Start();

            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.

            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            Console.WriteLine(output);
            Console.ReadLine();
        }
    }
}