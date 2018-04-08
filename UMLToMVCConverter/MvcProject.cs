namespace UMLToMVCConverter
{
    using System.IO;

    public class MvcProject
    {
        public string ProjectFolderPath { get; }
        
        public string Name => Path.GetFileName(this.ProjectFolderPath);

        public string ModelsFolderPath => Path.Combine(this.ProjectFolderPath, "Models");

        public string ViewsFolderPath => Path.Combine(this.ProjectFolderPath, "Views");

        public string CsprojFilePath => Path.Combine(this.ProjectFolderPath, this.Name + ".csproj");

        public MvcProject(string projectFolderPath)
        {
            this.ProjectFolderPath = projectFolderPath;
        }
    }
}
