﻿namespace UMLToEFConverter
{
    using System.IO;
    using System.Text;
    using UMLToEFConverter.Common;
    using UMLToEFConverter.Interfaces;
    using UMLToEFConverter.Models;

    public class StartupCsConfigurator : IStartupCsConfigurator
    {
        private const string AutogeneratedCodeBeginningMarker = "// UMLToEFConverter auto-generated code BEGIN";
        private const string AutogeneratedCodeEndingMarker = "// UMLToEFConverter auto-generated code END";
        private readonly MvcProject mvcProject;

        public StartupCsConfigurator(MvcProject mvcProject)
        {
            this.mvcProject = mvcProject;
        }

        public void SetUpStartupCsDbContextUse(string contextName)
        {
            var startupCsContent = File.ReadAllText(this.mvcProject.StartupCsPath);
            var startupCsCleared = ClearFile(startupCsContent);

            var outputStartupCsFileBuilder = new StringBuilder();
            outputStartupCsFileBuilder.AppendLine(AutogeneratedCodeBeginningMarker);
            outputStartupCsFileBuilder.AppendLine("using Microsoft.EntityFrameworkCore;");
            outputStartupCsFileBuilder.AppendLine($"using {this.mvcProject.Name}.Models;");
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

            File.WriteAllText(this.mvcProject.StartupCsPath, outputStartupCsFileBuilder.ToString());
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
