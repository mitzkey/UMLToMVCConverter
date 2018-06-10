namespace UMLToEFConverter.Common
{
    using System.Xml.Linq;

    public static class XElementExtensions
    {
        public static string ObligatoryAttributeValue(this XElement xElement, string attributeName)
        {
            var xAttribute = xElement.Attribute(attributeName);
            Insist.IsNotNull(xAttribute, nameof(xAttribute));

            var attributeValue = xAttribute.Value;
            Insist.IsNotNullOrWhiteSpace(attributeValue, nameof(attributeValue));

            return attributeValue;
        }

        public static string ObligatoryAttributeValue(this XElement xElement, XName attributeName)
        {
            var xAttribute = xElement.Attribute(attributeName);
            Insist.IsNotNull(xAttribute, nameof(xAttribute));

            var attributeValue = xAttribute.Value;
            Insist.IsNotNullOrWhiteSpace(attributeValue, nameof(attributeValue));

            return attributeValue;
        }

        public static string OptionalAttributeValue(this XElement xElement, string attributeName)
        {
            var xAttribute = xElement.Attribute(attributeName);

            var attributeValue = xAttribute?.Value;

            return attributeValue;
        }

        public static string OptionalAttributeValue(this XElement xElement, XName attributeName)
        {
            var xAttribute = xElement.Attribute(attributeName);

            var attributeValue = xAttribute?.Value;

            return attributeValue;
        }
    }
}