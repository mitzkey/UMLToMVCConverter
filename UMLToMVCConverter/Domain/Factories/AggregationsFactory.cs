namespace UMLToMVCConverter.Domain.Factories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Domain.Factories.Interfaces;
    using UMLToMVCConverter.Domain.Models;
    using UMLToMVCConverter.Domain.Repositories;
    using UMLToMVCConverter.Domain.Repositories.Interfaces;
    using UMLToMVCConverter.XmiTools;
    using UMLToMVCConverter.XmiTools.Interfaces;

    public class AggregationsFactory : IAggregationsFactory
    {
        private readonly IXmiWrapper xmiWrapper;
        private readonly ITypesRepository typesRepository;

        public AggregationsFactory(IXmiWrapper xmiWrapper, ITypesRepository typesRepository)
        {
            this.xmiWrapper = xmiWrapper;
            this.typesRepository = typesRepository;
        }

        public IEnumerable<Aggregation> Create(XElement xUmlModel)
        {
            var aggregations = new List<Aggregation>();

            var xAggregations = this.xmiWrapper.GetXAggregations(xUmlModel);

            foreach (var xAggregation in xAggregations)
            {
                var associationEnds = this.xmiWrapper.GetAssociationEndsXElements(xAggregation).ToList();

                var aggregationKindString = associationEnds
                    .Select(x => x.OptionalAttributeValue("aggregation"))
                    .Single(x => !string.IsNullOrWhiteSpace(x));

                var principalTypeAssociationXAttribute = associationEnds
                    .Single(x => !string.IsNullOrWhiteSpace(x.OptionalAttributeValue("aggregation")));

                var dependentTypeAssociationXAttribute = associationEnds
                    .Single(x => !x.Equals(principalTypeAssociationXAttribute));

                var principalTypeId = this.xmiWrapper.GetElementsId(principalTypeAssociationXAttribute.Parent);

                var principalType = this.typesRepository.GetTypeByXmiId(principalTypeId);

                var principalTypeMultiplicity = this.xmiWrapper.GetMultiplicity(dependentTypeAssociationXAttribute);

                var dependentTypeId = this.xmiWrapper.GetElementsId(dependentTypeAssociationXAttribute.Parent);

                var dependentType = this.typesRepository.GetTypeByXmiId(dependentTypeId);

                var dependentTypeMultiplicity = this.xmiWrapper.GetMultiplicity(principalTypeAssociationXAttribute);

                var aggregationKind = this.xmiWrapper.GetAggregationKind(aggregationKindString);
                
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
    }
}