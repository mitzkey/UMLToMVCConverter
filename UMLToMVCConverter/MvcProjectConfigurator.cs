namespace UMLToMVCConverter
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using UMLToMVCConverter.CodeTemplates;
    using UMLToMVCConverter.ExtensionMethods;

    public class MvcProjectConfigurator : IMvcProjectConfigurator
    {
        private readonly string modelsOutputPath;
        private readonly string appsettingJsonPath;
        private readonly string connectionString;

        public MvcProjectConfigurator(string mvcProjectPath, string connectionString)
        {
            Insist.IsNotNullOrWhiteSpace(mvcProjectPath, nameof(mvcProjectPath));
            this.modelsOutputPath = Path.Combine(mvcProjectPath, @"Models");
            this.appsettingJsonPath = Path.Combine(mvcProjectPath, "appsettings.json");
            this.connectionString = connectionString;
        }

        public void SetUpMvcProject(List<CodeTypeDeclaration> codeTypeDeclarations, string namespaceName)
        {
            Insist.IsNotNullOrWhiteSpace(namespaceName, nameof(namespaceName));

            this.SetUpDbConnection(namespaceName);
            this.GenerateModelsAndDbContext(codeTypeDeclarations, namespaceName);
        }

        private void SetUpDbConnection(string namespaceName)
        {
            var contextName = GetContextName(namespaceName);

            var appsettingsJsonContent = File.ReadAllText(this.appsettingJsonPath);
            var appsettingsJson = JObject.Parse(appsettingsJsonContent);

            var connectionStringConfig = new JObject
            {
                [contextName] = this.connectionString
            };

            var connectionStrings = new JProperty("ConnectionStrings", connectionStringConfig);

            appsettingsJson.AddOrUpdate(connectionStrings);

            var appsettingsJsonOutput = JsonConvert.SerializeObject(appsettingsJson, Formatting.Indented);

            File.WriteAllText(this.appsettingJsonPath, appsettingsJsonOutput);
        }

        private void GenerateModelsAndDbContext(IEnumerable<CodeTypeDeclaration> codeTypeDeclarations, string namespaceName)
        {
            PrepareFolder(this.modelsOutputPath);

            var codeTypeDeclarationsList = codeTypeDeclarations.ToList();
            foreach (var ctd in codeTypeDeclarationsList)
            {
                var tmpl = new ModelClassTextTemplate(ctd, namespaceName);
                var fileContent = tmpl.TransformText();
                var filesOutputPath = Path.Combine(this.modelsOutputPath, ctd.Name + ".cs");
                File.WriteAllText(filesOutputPath, fileContent);
            }

            var standaloneEntityTypes = codeTypeDeclarationsList.
                Where(i => !i.TypeAttributes.HasFlag(TypeAttributes.Abstract)
                           && !i.IsStruct)
                .ToList();

            this.GenerateDbContextClass(standaloneEntityTypes, namespaceName);
        }

        private void GenerateDbContextClass(List<CodeTypeDeclaration> codeTypeDeclarations, string namespaceName)
        {
            var tmpl = new DbContextTextTemplate(codeTypeDeclarations, namespaceName);
            var fileContent = tmpl.TransformText();
            var contextName = GetContextName(namespaceName);
            var fileOutputPath = Path.Combine(this.modelsOutputPath, contextName + ".cs");
            File.WriteAllText(fileOutputPath, fileContent);
        }

        private static string GetContextName(string namespaceName)
        {
            return namespaceName + "Context";
        }

        private static void PrepareFolder(string path)
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
