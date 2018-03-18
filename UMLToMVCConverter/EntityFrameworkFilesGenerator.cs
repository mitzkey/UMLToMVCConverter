namespace UMLToMVCConverter
{
    using System.CodeDom;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using UMLToMVCConverter.CodeTemplates;

    public class EntityFrameworkFilesGenerator : IFilesGenerator
    {
        private readonly string modelsOutputPath;

        public EntityFrameworkFilesGenerator(string mvcProjectPath)
        {
            Insist.IsNotNullOrWhiteSpace(mvcProjectPath, nameof(mvcProjectPath));
            this.modelsOutputPath = Path.Combine(mvcProjectPath, @"Models");
        }

        public void GenerateFiles(List<CodeTypeDeclaration> typesToGenerate, string namespaceNameToGenerate)
        {
            this.GenerateModelsAndDbContext(typesToGenerate, namespaceNameToGenerate);
        }

        private void GenerateModelsAndDbContext(IEnumerable<CodeTypeDeclaration> codeTypeDeclarations, string contextName)
        {
            PrepareFolder(this.modelsOutputPath);

            var codeTypeDeclarationsList = codeTypeDeclarations.ToList();
            foreach (var ctd in codeTypeDeclarationsList)
            {
                var tmpl = new ModelClassTextTemplate(ctd, contextName);
                var fileContent = tmpl.TransformText();
                var filesOutputPath = Path.Combine(this.modelsOutputPath, ctd.Name + ".cs");
                File.WriteAllText(filesOutputPath, fileContent);
            }

            var standaloneEntityTypes = codeTypeDeclarationsList.
                Where(i => !i.TypeAttributes.HasFlag(TypeAttributes.Abstract)
                           && !i.IsStruct)
                .ToList();

            this.GenerateDbContextClass(standaloneEntityTypes, contextName);
        }

        private void GenerateDbContextClass(List<CodeTypeDeclaration> codeTypeDeclarations, string contextName)
        {
            var tmpl = new DbContextTextTemplate(codeTypeDeclarations, contextName);
            var fileContent = tmpl.TransformText();
            var fileOutputPath = Path.Combine(this.modelsOutputPath, contextName + "Context.cs");
            File.WriteAllText(fileOutputPath, fileContent);
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
