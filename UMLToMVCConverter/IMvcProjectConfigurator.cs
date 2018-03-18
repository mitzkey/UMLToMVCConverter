namespace UMLToMVCConverter
{
    using System.CodeDom;
    using System.Collections.Generic;

    public interface IMvcProjectConfigurator
    {
        void SetUpMvcProject(List<CodeTypeDeclaration> codeTypeDeclarations, string namespaceName);
    }
}