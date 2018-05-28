namespace UMLToMVCConverter.Generators.Deserializers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Common.XmiTools;
    using UMLToMVCConverter.Common.XmiTools.Interfaces;
    using UMLToMVCConverter.Generators.Deserializers.Interfaces;
    using UMLToMVCConverter.Models;
    using UMLToMVCConverter.Models.Repositories.Interfaces;

    public class AssociationDeserializer : IAssociationDeserializer
    {
        private readonly IXmiWrapper xmiWrapper;
        private readonly IXAttributeNameResolver xAttributeNameResolver;
        private readonly ITypesRepository typesRepository;

        public AssociationDeserializer(IXmiWrapper xmiWrapper, IXAttributeNameResolver xAttributeNameResolver, ITypesRepository typesRepository)
        {
            this.xmiWrapper = xmiWrapper;
            this.xAttributeNameResolver = xAttributeNameResolver;
            this.typesRepository = typesRepository;
        }

        public Association Create(XElement xAssociation)
        {
            var associationEndsXElements = this.xmiWrapper.GetAssociationEndsXElements(xAssociation).ToList();
            var xmiID = this.xmiWrapper.GetElementsId(xAssociation);

            this.typesRepository.TryGetTypeByXmiId(xmiID, out var associationClass);

            var associationMebers = this.CreateAssociationMembers(associationEndsXElements);

            return new Association(associationMebers, xmiID, associationClass);
        }

        private IEnumerable<AssociationEndMember> CreateAssociationMembers(List<XElement> associationEndsXElements)
        {
            var associationMembers = new List<AssociationEndMember>();
            foreach (var associationEndXElement in associationEndsXElements)
            {
                var xmiId = this.xmiWrapper.GetElementsId(associationEndXElement);
                var multiplicity = this.xmiWrapper.GetMultiplicity(associationEndXElement);
                var name = this.xAttributeNameResolver.GetAssociationsEndName(associationEndXElement);
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