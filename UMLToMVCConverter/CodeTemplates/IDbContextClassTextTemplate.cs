namespace UMLToMVCConverter.CodeTemplates
{
    using System.Collections.Generic;
    using UMLToMVCConverter.ExtendedTypes;

    public interface IDbContextClassTextTemplate
    {
        string TransformText(IEnumerable<ExtendedCodeTypeDeclaration> standaloneEntityTypes, IEnumerable<EFRelationshipModel> relationshipModels);
    }
}