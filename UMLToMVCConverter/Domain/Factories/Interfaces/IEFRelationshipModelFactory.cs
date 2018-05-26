namespace UMLToMVCConverter.Domain.Factories.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public interface IEFRelationshipModelFactory
    {
        IEnumerable<EFRelationship> Create(IEnumerable<Aggregation> aggregations);
    }
}