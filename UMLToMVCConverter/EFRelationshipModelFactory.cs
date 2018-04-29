namespace UMLToMVCConverter
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
                switch (aggregation.AggregationKind)
                {
                    case AggregationKinds.Composition:
                        var foreignKeyPropertyNames = aggregation.DependentType.ForeignKeys.Keys;
                        var multiplicity = new RelationshipMultiplicity
                        {
                            Name = "One",
                            IsObligatory = false
                        };

                        models.Add(new EFRelationshipModel(foreignKeyPropertyNames)
                        {
                            DeleteBehavior = "Cascade",
                            Multiplicity = multiplicity,
                            SourceEntityName = aggregation.PrincipalType.Name,
                            TargetEntityName = aggregation.DependentType.Name
                        });
                        break;
                }
            }

            return models;
        }
    }
}