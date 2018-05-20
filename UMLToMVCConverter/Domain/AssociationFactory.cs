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

        public AssociationFactory(IXmiWrapper xmiWrapper, IXAttributeNameResolver xAttributeNameResolver)
        {
            this.xmiWrapper = xmiWrapper;
            this.xAttributeNameResolver = xAttributeNameResolver;
        }

        public Association Create(XElement xAssociation)
        {
            var associationEndsXElements = this.xmiWrapper.GetAssociationEndsXElements(xAssociation).ToList();

            var associationMebers = this.CreateAssociationMembers(associationEndsXElements);

            return new Association(associationMebers);
        }

        private IEnumerable<AssociationEndMember> CreateAssociationMembers(List<XElement> associationEndsXElements)
        {
            foreach (var associationEndsXElement in associationEndsXElements)
            {
                var xmiId = this.xmiWrapper.GetElementsId(associationEndsXElement);
                var multiplicity = this.xmiWrapper.GetMultiplicity(associationEndsXElement);
                var name = this.xAttributeNameResolver.GetName(associationEndsXElement);
                
                yield return new AssociationEndMember(xmiId, name, multiplicity);
            }
        }
    }
}