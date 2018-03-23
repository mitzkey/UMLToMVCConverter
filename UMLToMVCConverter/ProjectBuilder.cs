namespace UMLToMVCConverter
{
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

            project.Build();

            var outputPath = Path.Combine(projectFolderPath, project.Properties.Single(p => p.Name == "OutputPath").EvaluatedValue);

            var assemblyPath = Path.Combine(outputPath, projectName + ".dll");

            return Assembly.LoadFrom(assemblyPath);
        }
    }
}