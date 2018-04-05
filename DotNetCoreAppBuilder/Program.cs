using System;

namespace DotNetCoreAppBuilder
{
    using System.Diagnostics;
    using System.IO;

    public class Program
    {
        static void Main()
        {
            var projectFile =
                @"C:\Users\mikolaj.bochajczuk\Desktop\priv\ConsoleApplication1\ConsoleApplication1\ConsoleApplication1.csproj";

            var outputPath =
                @"C:\Users\mikolaj.bochajczuk\Desktop\priv\Build output";

            RunBuildScript(projectFile, outputPath);

            Console.ReadLine();
        }

        private static void RunBuildScript(string projectPath, string outputPath)
        {
            var scriptName = "run.bat";

            File.WriteAllText(scriptName, $"dotnet msbuild {projectPath} /p:OutputPath=\"{outputPath}\"");
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
