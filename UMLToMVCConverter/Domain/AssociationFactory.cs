namespace UMLToMVCConverter.Domain
{
    using System.Xml.Linq;
    using UMLToMVCConverter.XmiTools;

    public class AssociationFactory : IAssociationFactory
    {
        private readonly IXmiWrapper xmiWrapper;

        public AssociationFactory(IXmiWrapper xmiWrapper)
        {
            this.xmiWrapper = xmiWrapper;
        }

        public Association Create(XElement xAssociation)
        {
            var associationEndsXElements = this.xmiWrapper.GetAssociationEndsXElements(xAssociation);

            return new Association(associationEndsXElements);
        }
    }
}