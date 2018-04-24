namespace UMLToMVCConverter.CodeTemplates
{
    using System.Collections.Generic;
    using UMLToMVCConverter.ExtendedTypes;
    using UMLToMVCConverter.Interfaces;

    public interface IDbContextClassTextTemplate
    {
        string TransformText(IEnumerable<ExtendedCodeTypeDeclaration> standaloneEntityTypes, IEnumerable<IRelationship> relationships);
    }
}