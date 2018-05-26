namespace UMLToMVCConverter.CodeTemplates.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public interface IDbContextClassTextTemplate
    {
        string TransformText(IEnumerable<TypeModel> standaloneEntityTypes, IEnumerable<EFRelationship> relationshipModels, IEnumerable<TypeModel> structs);
    }
}