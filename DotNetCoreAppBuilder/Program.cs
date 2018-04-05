using System;

namespace DotNetCoreAppBuilder
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Build.Construction;
    using Microsoft.Build.Evaluation;
    using Microsoft.Build.Framework;

    class Program
    {
        private static readonly string[] s_msBuildAssemblies =
        {
            "Microsoft.Build", "Microsoft.Build.Framework", "Microsoft.Build.Tasks.Core",
            "Microsoft.Build.Utilities.Core"
        };

        static void Main(string[] args)
        {
            var instanceToUse = RegisterDefaults();

            RegisterInstance(instanceToUse);


            var projectFile =
                @"C:\Users\mikolaj.bochajczuk\Desktop\priv\ConsoleApplication1\ConsoleApplication1\ConsoleApplication1.csproj";

            RunBuildScript(projectFile);
            var pre = ProjectRootElement.Open(projectFile);
            var project = new Project(pre);
            project.Build(new Logger());

            Console.ReadLine();
        }

        public static VisualStudioInstance RegisterDefaults()
        {
            var instance = GetInstances().FirstOrDefault();
            RegisterInstance(instance);

            return instance;
        }

        public static void RegisterInstance(VisualStudioInstance instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            var loadedMSBuildAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(IsMSBuildAssembly);
            /*if (loadedMSBuildAssemblies.Any())
            {
                var loadedAssemblyList = string.Join(Environment.NewLine, loadedMSBuildAssemblies.Select(a => a.GetName()));

                var error = $"{typeof(Program)}.{nameof(RegisterInstance)} was called, but MSBuild assemblies were already loaded." +
                            Environment.NewLine +
                            $"Ensure that {nameof(RegisterInstance)} is called before any method that directly references types in the Microsoft.Build namespace has been called." +
                            Environment.NewLine +
                            "Loaded MSBuild assemblies: " +
                            loadedAssemblyList;

                throw new InvalidOperationException(error);
            }*/

            AppDomain.CurrentDomain.AssemblyResolve += (_, eventArgs) =>
            {
                var assemblyName = new AssemblyName(eventArgs.Name);
                if (IsMSBuildAssembly(assemblyName))
                {
                    var targetAssembly = Path.Combine(instance.MSBuildPath, assemblyName.Name + ".dll");
                    return File.Exists(targetAssembly) ? Assembly.LoadFrom(targetAssembly) : null;
                }

                return null;
            };
        }

        private class Logger : ILogger
        {
            public void Initialize(IEventSource eventSource)
            {
                eventSource.AnyEventRaised += (_, args) => { Console.WriteLine(args.Message); };
            }

            public void Shutdown()
            {
            }

            public LoggerVerbosity Verbosity { get; set; }
            public string Parameters { get; set; }
        }

        private static IEnumerable<VisualStudioInstance> GetInstances()
        {
            var devConsole = GetDevConsoleInstance();
            if (devConsole != null)
                yield return devConsole;

            foreach (var instance in VisualStudioLocationHelper.GetInstances())
                yield return instance;
        }

        private static VisualStudioInstance GetDevConsoleInstance()
        {
            var path = Environment.GetEnvironmentVariable("VSINSTALLDIR");
            if (!string.IsNullOrEmpty(path))
            {
                var versionString = Environment.GetEnvironmentVariable("VSCMD_VER");
                Version version;
                Version.TryParse(versionString, out version);

                if (version == null)
                {
                    versionString = Environment.GetEnvironmentVariable("VisualStudioVersion");
                    Version.TryParse(versionString, out version);
                }

                return new VisualStudioInstance("DEVCONSOLE", path, version, DiscoveryType.DeveloperConsole);
            }

            return null;
        }

        private static bool IsMSBuildAssembly(Assembly assembly)
        {
            return IsMSBuildAssembly(assembly.GetName());
        }

        private static bool IsMSBuildAssembly(AssemblyName assemblyName)
        {
            return s_msBuildAssemblies.Contains(assemblyName.Name, StringComparer.OrdinalIgnoreCase);
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
