namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Domain.Models;
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
            var associationMembers = new List<AssociationEndMember>();
            foreach (var associationEndXElement in associationEndsXElements)
            {
                var xmiId = this.xmiWrapper.GetElementsId(associationEndXElement);
                var multiplicity = this.xmiWrapper.GetMultiplicity(associationEndXElement);
                var name = this.xAttributeNameResolver.GetName(associationEndXElement);
                var aggregationKindString = associationEndXElement.OptionalAttributeValue("aggregation");
                var aggregationKind = this.xmiWrapper.GetAggregationKind(aggregationKindString);

                var navigable = true;
                TypeModel owningType;
                var xOwner = this.xmiWrapper.GetXOwner(associationEndXElement);
                if (associationEndXElement.Name == "ownedEnd"
                    && (this.xmiWrapper.GetXElementType(xOwner) == XElementType.Association
                        || this.xmiWrapper.GetXElementType(xOwner) == XElementType.AssociationClass))
                {
                    navigable = false;
                    var xOwningType = this.xmiWrapper.GetAssociationsEndOwningType(associationEndXElement);
                    var xOwningTypeXmiId = this.xmiWrapper.GetElementsId(xOwningType);
                    owningType = this.typesRepository.GetTypeByXmiId(xOwningTypeXmiId);
                }
                else
                {
                    var xOwningTypeXmiId = this.xmiWrapper.GetElementsId(xOwner);
                    owningType = this.typesRepository.GetTypeByXmiId(xOwningTypeXmiId);
                }
                
                associationMembers.Add(new AssociationEndMember(xmiId, name, multiplicity, aggregationKind, owningType, navigable));
            }

            return associationMembers;
        }
    }
}