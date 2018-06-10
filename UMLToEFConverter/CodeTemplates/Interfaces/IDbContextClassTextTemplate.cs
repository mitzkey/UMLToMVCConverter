namespace UMLToEFConverter.CodeTemplates.Interfaces
{
    using System.Collections.Generic;
    using UMLToEFConverter.Models;

    public interface IDbContextClassTextTemplate
    {
        string TransformText(IEnumerable<TypeModel> standaloneEntityTypes, IEnumerable<EFRelationship> relationshipModels, IEnumerable<TypeModel> structs);
    }
}