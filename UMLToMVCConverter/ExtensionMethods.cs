namespace UMLToMVCConverter
{
    using System.Linq;
    using System.Text;
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

        public static string NewSection(this string text)
        {
            Insist.IsNotNull(text, nameof(text));
            return text.Replace("\n", "\n\t");
        }

        public static string AddNewLines(this string text, int amount)
        {
            var stringbuilder = new StringBuilder(string.Empty);

            for (int i = 0; i < amount; i++)
            {
                stringbuilder.AppendLine(string.Empty);
            }

            stringbuilder.Append(text);

            return stringbuilder.ToString();

        }

        public static string AddTabs(this string text, int amount)
        {
            var stringbuilder = new StringBuilder(string.Empty);

            for (int i = 0; i < amount; i++)
            {
                stringbuilder.Append("\t");
            }

            stringbuilder.Append(text);

            return stringbuilder.ToString();

        }

        public static string FirstCharToUpper(this string input)
        {
            Insist.IsNotNullOrWhiteSpace(input, nameof(input));
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
