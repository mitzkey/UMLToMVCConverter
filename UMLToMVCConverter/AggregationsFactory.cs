namespace UMLToMVCConverter
{
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

                var aggregationKind = associationEnds.Item1.OptionalAttributeValue("aggregation")
                                      ?? associationEnds.Item2.OptionalAttributeValue("aggregation");
                
                if (aggregationKind == "composite")
                {
                    var principalTypeAssociationXAttribute =
                        string.IsNullOrWhiteSpace(associationEnds.Item1.OptionalAttributeValue("aggregation"))
                            ? associationEnds.Item2
                            : associationEnds.Item1;

                    var dependentTypeAssociationXAttribute = associationEnds.Item1.Equals(principalTypeAssociationXAttribute)
                        ? associationEnds.Item2
                        : associationEnds.Item1;

                    var principalTypeId = this.xmiWrapper.GetElementsId(principalTypeAssociationXAttribute.Parent);

                    var principalType = typesList.Single(x => x.XmiID == principalTypeId);
                    
                    var principalTypeMultiplicity = this.xmiWrapper.GetMultiplicity(principalTypeAssociationXAttribute);

                    var dependentTypeId = this.xmiWrapper.GetElementsId(dependentTypeAssociationXAttribute.Parent);

                    var dependentType = typesList.Single(x => x.XmiID == dependentTypeId);

                    var dependentTypeMultiplicity = this.xmiWrapper.GetMultiplicity(dependentTypeAssociationXAttribute);

                    aggregations.Add(
                        new Aggregation
                        {
                            AggregationKind = AggregationKinds.Composition,
                            PrincipalType = principalType,
                            PrincipalTypeMultiplicity = principalTypeMultiplicity,
                            PrincipalTypeAssociationXAttribute = principalTypeAssociationXAttribute,
                            DependentType = dependentType,
                            DependentTypeMultiplicity = dependentTypeMultiplicity,
                            DependentTypeAssociationXAttribute = dependentTypeAssociationXAttribute
                        });
                }
            }

            return aggregations;
        }
    }
}