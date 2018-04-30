namespace UMLToMVCConverter
{
    using System;
    using System.Collections.Generic;
    using UMLToMVCConverter.Interfaces;

    public class EFRelationshipModelFactory : IEFRelationshipModelFactory
    {
        public IEnumerable<EFRelationshipModel> Create(IEnumerable<Aggregation> aggregations)
        {
            var models = new List<EFRelationshipModel>();

            foreach (var aggregation in aggregations)
            {
                switch (aggregation.AggregationKind)
                {
                    case AggregationKinds.Composition:
                        var foreignKeyPropertyNames = aggregation.DependentType.ForeignKeys.Keys;

                        var principalTypeMultiplicity = this.GetRelationshipMultiplicity(aggregation.PrincipalTypeMultiplicity);
                        var dependentTypeMultiplicity = this.GetRelationshipMultiplicity(aggregation.DependentTypeMultiplicity);

                        models.Add(new EFRelationshipModel(foreignKeyPropertyNames)
                        {
                            DeleteBehavior = "Cascade",
                            PrincipalTypeMultiplicity = principalTypeMultiplicity,
                            DependentTypeMultiplicity = dependentTypeMultiplicity,
                            PrincipalTypeName = aggregation.PrincipalType.Name,
                            DependentTypeName = aggregation.DependentType.Name
                        });
                        break;
                }
            }

            return models;
        }

        private RelationshipMultiplicity GetRelationshipMultiplicity(Multiplicity multiplicity)
        {
            return new RelationshipMultiplicity
            {
                Multiplicity = multiplicity
            };
        }
    }
}