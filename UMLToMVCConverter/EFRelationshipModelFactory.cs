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
            string name;
            bool isObligatory;

            switch (multiplicity)
            {
                case Multiplicity.ExactlyOne:
                    name = "One";
                    isObligatory = true;
                    break;
                case Multiplicity.ZeroOrOne:
                    name = "One";
                    isObligatory = false;
                    break;
                case Multiplicity.ZeroOrMore:
                    name = "Many";
                    isObligatory = false;
                    break;
                case Multiplicity.OneOrMore:
                    name = "Many";
                    isObligatory = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(multiplicity), multiplicity, null);
            }

            return new RelationshipMultiplicity
            {
                Name = name,
                IsObligatory = isObligatory
            };
        }
    }
}