namespace UMLToMVCConverter.Common
{
    using System;
    using System.Linq;
    using System.Text;

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
    }
}