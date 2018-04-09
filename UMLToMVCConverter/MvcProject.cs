﻿namespace UMLToMVCConverter
{
    using System.IO;

    public class MvcProject : IMvcProject
    {
        public string ProjectFolderPath { get; }
        
        public string Name => Path.GetFileName(this.ProjectFolderPath);

        public string ModelsFolderPath => Path.Combine(this.ProjectFolderPath, "Models");

        public string ViewsFolderPath => Path.Combine(this.ProjectFolderPath, "Views");

        public string DbContextPrefix { get; }

        public string WorkspaceFolderPath { get; }

        public string DbConnectionString { get; }

        public string DbContextName => this.DbContextPrefix + "Context";

        public string StartupCsPath => Path.Combine(this.ProjectFolderPath, "Startup.cs");

        public string DefaultNamespace => this.Name;

        public string AssemblyPath => Path.Combine(this.WorkspaceFolderPath, this.Name + ".dll");

        public string CsprojFilePath => Path.Combine(this.ProjectFolderPath, this.Name + ".csproj");

        public MvcProject(string projectFolderPath, string defaultNamespaceName, string workspaceFolderPath, string dbConnectionString)
        {
            this.ProjectFolderPath = projectFolderPath;
            this.DbContextPrefix = defaultNamespaceName;
            this.WorkspaceFolderPath = workspaceFolderPath;
            this.DbConnectionString = dbConnectionString;
        }
    }
}
