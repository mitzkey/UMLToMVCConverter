namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public interface IForeignKeysGenerator
    {
        void Generate(IEnumerable<Aggregation> aggregations);
        void Generate(AssociationEndMember dependentMember, AssociationEndMember principalMember);
    }
}