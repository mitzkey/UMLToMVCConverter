namespace UMLToMVCConverter.CodeTemplates
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain;

    public interface IDbContextClassTextTemplate
    {
        string TransformText(IEnumerable<ExtendedCodeTypeDeclaration> standaloneEntityTypes, IEnumerable<EFRelationshipModel> relationshipModels, IEnumerable<ExtendedCodeTypeDeclaration> structs);
    }
}