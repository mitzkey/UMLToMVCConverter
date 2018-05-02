namespace UMLToMVCConverter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;
    using UMLToMVCConverter.ExtensionMethods;
    using UMLToMVCConverter.Interfaces;

    public class AggregationsFactory : IAggregationsFactory
    {
        private readonly IXmiWrapper xmiWrapper;

        public AggregationsFactory(IXmiWrapper xmiWrapper)
        {
            this.xmiWrapper = xmiWrapper;
        }

        public IEnumerable<Aggregation> Create(XElement xUmlModel, IEnumerable<ExtendedCodeTypeDeclaration> types)
        {
            var aggregations = new List<Aggregation>();

            var xAggregations = this.xmiWrapper.GetXAggregations(xUmlModel);

            var typesList = types.ToList();

            foreach (var xAggregation in xAggregations)
            {
                var associationEnds = this.xmiWrapper.GetAssociationEnds(xAggregation);

                var aggregationKindString = associationEnds.Item1.OptionalAttributeValue("aggregation")
                                      ?? associationEnds.Item2.OptionalAttributeValue("aggregation");

                var principalTypeAssociationXAttribute =
                    string.IsNullOrWhiteSpace(associationEnds.Item1.OptionalAttributeValue("aggregation"))
                        ? associationEnds.Item2
                        : associationEnds.Item1;

                var dependentTypeAssociationXAttribute = associationEnds.Item1.Equals(principalTypeAssociationXAttribute)
                    ? associationEnds.Item2
                    : associationEnds.Item1;

                var principalTypeId = this.xmiWrapper.GetElementsId(principalTypeAssociationXAttribute.Parent);

                var principalType = typesList.Single(x => x.XmiID == principalTypeId);

                var principalTypeMultiplicity = this.xmiWrapper.GetMultiplicity(dependentTypeAssociationXAttribute);

                var dependentTypeId = this.xmiWrapper.GetElementsId(dependentTypeAssociationXAttribute.Parent);

                var dependentType = typesList.Single(x => x.XmiID == dependentTypeId);

                var dependentTypeMultiplicity = this.xmiWrapper.GetMultiplicity(principalTypeAssociationXAttribute);

                var aggregationKind = this.GetAggregationKind(aggregationKindString);
                
                aggregations.Add(
                    new Aggregation
                    {
                        AggregationKind = aggregationKind,
                        PrincipalType = principalType,
                        PrincipalTypeMultiplicity = principalTypeMultiplicity,
                        PrincipalTypeAssociationXAttribute = principalTypeAssociationXAttribute,
                        DependentType = dependentType,
                        DependentTypeMultiplicity = dependentTypeMultiplicity,
                        DependentTypeAssociationXAttribute = dependentTypeAssociationXAttribute
                    });
            }

            return aggregations;
        }

        private AggregationKinds GetAggregationKind(string aggregationKindString)
        {
            switch (aggregationKindString)
            {
                case "composite":
                    return AggregationKinds.Composition;
                case "shared":
                    return AggregationKinds.Shared;
                default:
                    throw new NotImplementedException($"No aggregation kind implemented for value: {aggregationKindString}");
            }
        }
    }
}