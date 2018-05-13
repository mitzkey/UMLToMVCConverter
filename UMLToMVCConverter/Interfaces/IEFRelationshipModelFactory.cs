namespace UMLToMVCConverter.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Models;

    public interface IEFRelationshipModelFactory
    {
        IEnumerable<EFRelationshipModel> Create(IEnumerable<Aggregation> aggregations);
    }
}