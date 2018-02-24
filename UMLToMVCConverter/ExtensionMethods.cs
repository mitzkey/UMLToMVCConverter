namespace UMLToMVCConverter
{
    using System.Xml.Linq;

    public static class MyExtensions
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
    }
}
