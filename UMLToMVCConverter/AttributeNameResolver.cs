namespace UMLToMVCConverter
{
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtensionMethods;
    using UMLToMVCConverter.Interfaces;

    public class AttributeNameResolver : IAttributeNameResolver
    {
        private readonly IXmiWrapper xmiWrapper;

        public AttributeNameResolver(IXmiWrapper xmiWrapper)
        {
            this.xmiWrapper = xmiWrapper;
        }

        public string GetName(XElement attribute)
        {
            var associationId = attribute.OptionalAttributeValue("association");
            if (string.IsNullOrWhiteSpace(associationId))
            {
                return attribute.ObligatoryAttributeValue("name").FirstCharToUpper();
            }

            return GetNameForAggregation(attribute);
        }

        private string GetNameForAggregation(XElement attribute)
        {
            var ownedTypeId = attribute.ObligatoryAttributeValue("type");
            var type = this.xmiWrapper.GetXElementById(ownedTypeId);

            return type.ObligatoryAttributeValue("name");
        }
    }
}