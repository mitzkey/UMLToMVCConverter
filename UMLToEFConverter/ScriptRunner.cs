namespace UMLToEFConverter
{
    using System.Diagnostics;
    using System.IO;
    using UMLToEFConverter.Common;
    using UMLToEFConverter.Interfaces;

    public class ScriptRunner : IScriptRunner
    {
        private readonly ILogger logger;

        public ScriptRunner(ILogger logger)
        {
            this.logger = logger;
        }

        public void Run(string scriptName, string scriptContent)
        {
            File.WriteAllText(scriptName, scriptContent);
            var process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = scriptName
                }
            };

            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            this.logger.LogInfo(output);
        }
    }
}