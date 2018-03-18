namespace UMLToMVCConverter
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Remoting.Metadata.W3cXsd2001;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using UMLToMVCConverter.CodeTemplates;
    using UMLToMVCConverter.ExtensionMethods;

    public class MvcProjectConfigurator : IMvcProjectConfigurator
    {
        private readonly string modelsFolderPath;
        private readonly string connectionString;
        private readonly string mvcProjectName;
        private readonly string mvcProjectPath;

        public MvcProjectConfigurator(string mvcProjectPath, string connectionString)
        {
            Insist.IsNotNullOrWhiteSpace(mvcProjectPath, nameof(mvcProjectPath));
            Insist.IsNotNullOrWhiteSpace(connectionString, nameof(connectionString));

            this.mvcProjectPath = mvcProjectPath;
            this.modelsFolderPath = Path.Combine(this.mvcProjectPath, @"Models");
            this.connectionString = connectionString;
            this.mvcProjectName = Path.GetFileName(this.mvcProjectPath);
        }

        public void SetUpMvcProject(List<CodeTypeDeclaration> codeTypeDeclarations, string namespaceName)
        {
            Insist.IsNotNullOrWhiteSpace(namespaceName, nameof(namespaceName));

            this.SetUpDbConnection(namespaceName);
            PrepareEmptyFolder(this.modelsFolderPath);
            
            this.GenerateModels(codeTypeDeclarations);
            this.GenerateDbContextClass(codeTypeDeclarations, namespaceName);
        }

        private void SetUpDbConnection(string namespaceName)
        {
            var contextName = GetContextName(namespaceName);

            this.SetUpAppsettingsDbConnection(contextName);

            StartupCsConfigurator.SetUpStartupCsDbContextUse(contextName, this.mvcProjectPath);
        }

        private void SetUpAppsettingsDbConnection(string contextName)
        {
            var appsettingJsonPath = Path.Combine(this.mvcProjectPath, "appsettings.json");
            var appsettingsJsonContent = File.ReadAllText(appsettingJsonPath);
            var appsettingsJson = JObject.Parse(appsettingsJsonContent);

            var connectionStringConfig = new JObject
            {
                [contextName] = this.connectionString
            };

            var connectionStrings = new JProperty("ConnectionStrings", connectionStringConfig);

            appsettingsJson.AddOrUpdate(connectionStrings);

            var appsettingsJsonOutput = JsonConvert.SerializeObject(appsettingsJson, Formatting.Indented);

            File.WriteAllText(appsettingJsonPath, appsettingsJsonOutput);
        }

        private void GenerateModels(IEnumerable<CodeTypeDeclaration> codeTypeDeclarations)
        {
            var codeTypeDeclarationsList = codeTypeDeclarations.ToList();
            foreach (var ctd in codeTypeDeclarationsList)
            {
                var tmpl = new ModelClassTextTemplate(ctd, this.mvcProjectName);
                var fileContent = tmpl.TransformText();
                var filesOutputPath = Path.Combine(this.modelsFolderPath, ctd.Name + ".cs");
                File.WriteAllText(filesOutputPath, fileContent);
            }
        }

        private void GenerateDbContextClass(List<CodeTypeDeclaration> codeTypeDeclarations, string namespaceName)
        {
            var standaloneEntityTypes = codeTypeDeclarations.
                Where(i => !i.TypeAttributes.HasFlag(TypeAttributes.Abstract)
                           && !i.IsStruct)
                .ToList();

            var contextName = GetContextName(namespaceName);

            var tmpl = new DbContextTextTemplate(standaloneEntityTypes, contextName, this.mvcProjectName);
            var fileContent = tmpl.TransformText();
            var fileOutputPath = Path.Combine(this.modelsFolderPath, contextName + ".cs");
            File.WriteAllText(fileOutputPath, fileContent);
        }

        private static string GetContextName(string namespaceName)
        {
            return namespaceName + "Context";
        }

        private static void PrepareEmptyFolder(string path)
        {
            if (Directory.Exists(path))
            {
                ClearFolder(path);
                return;
            }

            Directory.CreateDirectory(path);
        }

        private static void ClearFolder(string path)
        {
            var directory = new DirectoryInfo(path);

            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }

            foreach (var subDirectory in directory.GetDirectories())
            {
                ClearFolder(subDirectory.FullName);
                subDirectory.Delete();
            }
        }
    }
}
