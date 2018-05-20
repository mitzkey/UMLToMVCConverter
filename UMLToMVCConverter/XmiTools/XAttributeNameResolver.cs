namespace UMLToMVCConverter.XmiTools
{
    using System;
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;

    public class XAttributeNameResolver : IXAttributeNameResolver
    {
        private readonly IXmiWrapper xmiWrapper;

        public XAttributeNameResolver(IXmiWrapper xmiWrapper)
        {
            this.xmiWrapper = xmiWrapper;
        }

        public string GetName(XElement attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute.OptionalAttributeValue("name")))
            {
                var associationId = attribute.OptionalAttributeValue("association");
                if (!string.IsNullOrWhiteSpace(associationId))
                {
                    return this.GetNameForAggregation(attribute);
                }

                throw new NotImplementedException("Can't obtain attribute's name");
            }

            return attribute.ObligatoryAttributeValue("name").FirstCharToUpper();
        }

        private string GetNameForAggregation(XElement attribute)
        {
            var ownedTypeId = attribute.ObligatoryAttributeValue("type");
            var type = this.xmiWrapper.GetXElementById(ownedTypeId);

            return type.ObligatoryAttributeValue("name");
        }
    }
}