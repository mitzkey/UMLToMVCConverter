namespace UMLToMVCConverter.Domain.Generators.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public interface IForeignKeysGenerator
    {
        void Generate(IEnumerable<Aggregation> aggregations);
        void Generate(AssociationEndMember sourceMember, AssociationEndMember destinationMember);
    }
}