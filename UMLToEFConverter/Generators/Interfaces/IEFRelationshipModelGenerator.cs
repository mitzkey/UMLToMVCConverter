namespace UMLToEFConverter.Generators.Interfaces
{
    using System.Collections.Generic;
    using UMLToEFConverter.Models;

    public interface IEFRelationshipModelGenerator
    {
        IEnumerable<EFRelationship> CreateRelationshipsConfiguratingOnDeleteBehaviour(IEnumerable<Association> associations);
    }
}