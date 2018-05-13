namespace UMLToMVCConverter.XmiTools
{
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Interfaces;

    public class XAttributeNameResolver : IXAttributeNameResolver
    {
        private readonly IXmiWrapper xmiWrapper;

        public XAttributeNameResolver(IXmiWrapper xmiWrapper)
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

            return this.GetNameForAggregation(attribute);
        }

        private string GetNameForAggregation(XElement attribute)
        {
            var ownedTypeId = attribute.ObligatoryAttributeValue("type");
            var type = this.xmiWrapper.GetXElementById(ownedTypeId);

            return type.ObligatoryAttributeValue("name");
        }
    }
}