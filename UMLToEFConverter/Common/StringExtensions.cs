namespace UMLToEFConverter.Common
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
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

        public static string[] AsArrayOfLines(this string input)
        {
            return input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ToCamelCase(this string input)
        {
            var sb = new StringBuilder();
            foreach (var word in Regex.Split(input, @"\s+"))
            {
                sb.Append(word.FirstCharToUpper());
            }
            return sb.ToString();
        }
    }
}