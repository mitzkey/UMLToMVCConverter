namespace UMLToMVCConverter
{
    using System.CodeDom;
    using System.Collections.Generic;

    public interface IFilesGenerator
    {
        void SetUpMvcProject(List<CodeTypeDeclaration> codeTypeDeclarations, string contextName);
    }
}