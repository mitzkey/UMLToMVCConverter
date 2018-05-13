namespace UMLToMVCConverter.Models
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Interfaces;

    public class EFRelationshipModelFactory : IEFRelationshipModelFactory
    {
        public IEnumerable<EFRelationshipModel> Create(IEnumerable<Aggregation> aggregations)
        {
            var models = new List<EFRelationshipModel>();

            foreach (var aggregation in aggregations)
            {
                var deleteBehavior = aggregation.AggregationKind == AggregationKinds.Composition
                    ? "Cascade"
                    : "Restrict";

                var foreignKeyPropertyNames = aggregation.DependentType.ForeignKeys.Keys;

                var principalTypeMultiplicity = GetRelationshipMultiplicity(aggregation.PrincipalTypeMultiplicity);
                var dependentTypeMultiplicity = GetRelationshipMultiplicity(aggregation.DependentTypeMultiplicity);

                models.Add(new EFRelationshipModel(foreignKeyPropertyNames)
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

        private static RelationshipMultiplicity GetRelationshipMultiplicity(Multiplicity multiplicity)
        {
            return new RelationshipMultiplicity
            {
                Multiplicity = multiplicity
            };
        }
    }
}