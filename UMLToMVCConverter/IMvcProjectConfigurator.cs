namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using UMLToMVCConverter.ExtendedTypes;

    public interface IMvcProjectConfigurator
    {
        void SetUpMvcProject(IEnumerable<ExtendedCodeTypeDeclaration> codeTypeDeclarations);
    }
}