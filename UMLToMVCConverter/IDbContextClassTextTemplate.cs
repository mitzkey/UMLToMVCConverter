namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using UMLToMVCConverter.ExtendedTypes;

    public interface IDbContextClassTextTemplate
    {
        string TransformText(List<ExtendedCodeTypeDeclaration> standaloneEntityTypes);
    }
}