namespace UMLToMVCConverter
{
    using System.CodeDom;
    using System.Collections.Generic;

    public interface IFilesGenerator
    {
        void GenerateFiles(List<CodeTypeDeclaration> typesToGenerate, string namespaceNameToGenerate);
    }
}