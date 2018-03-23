﻿namespace UMLToMVCConverter
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using UMLToMVCConverter.ExtensionMethods;

    public class StartupCsConfigurator : IStartupCsConfigurator
    {
        private const string AutogeneratedCodeBeginningMarker = "// UMLToMVCConverter auto-generated code BEGIN";
        private const string AutogeneratedCodeEndingMarker = "// UMLToMVCConverter auto-generated code END";
        private readonly string mvcProjectPath;

        public StartupCsConfigurator(string mvcProjectPath)
        {
            this.mvcProjectPath = mvcProjectPath;
        }

        public void SetUpStartupCsDbContextUse(string contextName)
        {
            var startupCsPath = Path.Combine(this.mvcProjectPath, "Startup.cs");

            var startupCsContent = File.ReadAllText(startupCsPath);

            var startupCsCleared = ClearFile(startupCsContent);

            var outputStartupCsFileBuilder = new StringBuilder();

            var projectName = this.mvcProjectPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries).Last();
            outputStartupCsFileBuilder.AppendLine(AutogeneratedCodeBeginningMarker);
            outputStartupCsFileBuilder.AppendLine("using Microsoft.EntityFrameworkCore;");
            outputStartupCsFileBuilder.AppendLine($"using {projectName}.Models;");
            outputStartupCsFileBuilder.AppendLine(AutogeneratedCodeEndingMarker);

            var distanceFromLineToInsert = int.MaxValue;
            foreach (var line in startupCsCleared.AsArrayOfLines())
            {
                distanceFromLineToInsert--;
                if (line.Contains("public void ConfigureServices(IServiceCollection services)"))
                {
                    distanceFromLineToInsert = 2;
                }

                if (distanceFromLineToInsert == 0)
                {
                    outputStartupCsFileBuilder.AppendLine(AutogeneratedCodeBeginningMarker);
                    outputStartupCsFileBuilder.AppendLine($"\t\t\tservices.AddDbContext<{contextName}>(");
                    outputStartupCsFileBuilder.AppendLine("\t\t\t\toptions =>");
                    outputStartupCsFileBuilder.AppendLine($"\t\t\t\t\toptions.UseSqlServer(this.Configuration.GetConnectionString(\"{contextName}\")));");
                    outputStartupCsFileBuilder.AppendLine(AutogeneratedCodeEndingMarker);
                }

                outputStartupCsFileBuilder.AppendLine(line);
            }

            File.WriteAllText(startupCsPath, outputStartupCsFileBuilder.ToString());
        }

        private static string ClearFile(string startupCsContent)
        {
            var output = new StringBuilder();
            var ignoring = false;
            var lines = startupCsContent.AsArrayOfLines();
            foreach (var line in lines)
            {
                if (line.Contains(AutogeneratedCodeBeginningMarker))
                {
                    ignoring = true;
                }

                if (!ignoring)
                {
                    output.AppendLine(line);
                }

                if (line.Contains(AutogeneratedCodeEndingMarker))
                {
                    ignoring = false;
                }
            }

            return output.ToString();
        }
    }
}
