namespace UMLToMVCConverter.XmiTools
{
    using System;
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.XmiTools.Interfaces;

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
                throw new NotImplementedException("Can't obtain attribute's name");
            }

            return attribute.ObligatoryAttributeValue("name").ToCamelCase();
        }

        public string GetAssociationsEndName(XElement associationEndXElement)
        {
            var name = associationEndXElement.OptionalAttributeValue("name");

            if (string.IsNullOrWhiteSpace(name))
            {
                var ownedTypeId = associationEndXElement.ObligatoryAttributeValue("type");
                var type = this.xmiWrapper.GetXElementById(ownedTypeId);
                name = type.ObligatoryAttributeValue("name");

                var associationXmiId = associationEndXElement.ObligatoryAttributeValue("association");
                var xAssociation = this.xmiWrapper.GetXElementById(associationXmiId);
                var associationName = xAssociation.OptionalAttributeValue("name");
                if (!string.IsNullOrWhiteSpace(associationName))
                {
                    name = name + associationName.FirstCharToUpper();
                }
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new NotImplementedException("Can't obtain association ends name");
            }

            return name.ToCamelCase();
        }
    }
}