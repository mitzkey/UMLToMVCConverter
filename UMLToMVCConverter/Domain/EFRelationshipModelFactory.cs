namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public class EFRelationshipModelFactory : IEFRelationshipModelFactory
    {
        public IEnumerable<EFRelationship> Create(IEnumerable<Aggregation> aggregations)
        {
            var models = new List<EFRelationship>();

            foreach (var aggregation in aggregations)
            {
                var deleteBehavior = aggregation.AggregationKind == AggregationKind.Composition
                    ? "Cascade"
                    : "SetNull";

                var foreignKeyPropertyNames = aggregation.DependentType.ForeignKeys.Keys;

                var principalTypeMultiplicity = GetRelationshipMultiplicity(aggregation.PrincipalTypeMultiplicity);
                var dependentTypeMultiplicity = GetRelationshipMultiplicity(aggregation.DependentTypeMultiplicity);

                models.Add(new EFRelationship(foreignKeyPropertyNames)
                {
                    DeleteBehavior = deleteBehavior,
                    PrincipalTypeMultiplicity = principalTypeMultiplicity,
                    DependentTypeMultiplicity = dependentTypeMultiplicity,
                    PrincipalTypeName = aggregation.PrincipalType.Name,
                    DependentTypeName = aggregation.DependentType.Name
                });
            }

            return models;
        }

        private static EFRelationshipMemberMultiplicity GetRelationshipMultiplicity(Multiplicity multiplicity)
        {
            return new EFRelationshipMemberMultiplicity
            {
                Multiplicity = multiplicity
            };
        }
    }
}