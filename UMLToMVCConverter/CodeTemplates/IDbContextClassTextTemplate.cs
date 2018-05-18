namespace UMLToMVCConverter.CodeTemplates
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain;
    using UMLToMVCConverter.Domain.Models;

    public interface IDbContextClassTextTemplate
    {
        string TransformText(IEnumerable<ExtendedCodeTypeDeclaration> standaloneEntityTypes, IEnumerable<EFRelationship> relationshipModels, IEnumerable<ExtendedCodeTypeDeclaration> structs);
    }
}