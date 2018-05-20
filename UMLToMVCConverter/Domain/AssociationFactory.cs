namespace UMLToMVCConverter.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.XmiTools;

    public class AssociationFactory : IAssociationFactory
    {
        private readonly IXmiWrapper xmiWrapper;
        private readonly IXAttributeNameResolver xAttributeNameResolver;
        private readonly ITypesRepository typesRepository;

        public AssociationFactory(IXmiWrapper xmiWrapper, IXAttributeNameResolver xAttributeNameResolver, ITypesRepository typesRepository)
        {
            this.xmiWrapper = xmiWrapper;
            this.xAttributeNameResolver = xAttributeNameResolver;
            this.typesRepository = typesRepository;
        }

        public Association Create(XElement xAssociation)
        {
            var associationEndsXElements = this.xmiWrapper.GetAssociationEndsXElements(xAssociation).ToList();
            var xmiID = this.xmiWrapper.GetElementsId(xAssociation);

            var associationMebers = this.CreateAssociationMembers(associationEndsXElements);

            return new Association(associationMebers, xmiID);
        }

        private IEnumerable<AssociationEndMember> CreateAssociationMembers(List<XElement> associationEndsXElements)
        {
            foreach (var associationEndsXElement in associationEndsXElements)
            {
                var xmiId = this.xmiWrapper.GetElementsId(associationEndsXElement);
                var multiplicity = this.xmiWrapper.GetMultiplicity(associationEndsXElement);
                var name = this.xAttributeNameResolver.GetName(associationEndsXElement);
                var aggregationKindString = associationEndsXElement.OptionalAttributeValue("aggregation");
                var aggregationKind = this.xmiWrapper.GetAggregationKind(aggregationKindString);

                var owningType = this.typesRepository.GetOwner(associationEndsXElement);
                
                yield return new AssociationEndMember(xmiId, name, multiplicity, aggregationKind, owningType);
            }
        }
    }
}