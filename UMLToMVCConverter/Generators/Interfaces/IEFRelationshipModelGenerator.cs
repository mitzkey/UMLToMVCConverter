namespace UMLToMVCConverter.Domain.Factories.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Models;

    public interface IEFRelationshipModelGenerator
    {
        IEnumerable<EFRelationship> CreateRelationshipsConfiguratingOnDeleteBehaviour(IEnumerable<Association> associations);
    }
}