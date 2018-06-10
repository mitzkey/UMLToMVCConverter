namespace UMLToEFConverter.Common
{
    using System;
    using JetBrains.Annotations;

    public static class Insist
    {
        private const string NotNullMessage = "The supplied argument cannot be null";
        private const string NotNullOrEmptyMessage = "The supplied argument cannot be null or empty";
        private const string NotNullOrWhitespaceMessage = "The supplied argument cannot be null or whitespace";

        [AssertionMethod]
        public static void IsNotNull([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] object value, string argumentName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName ?? "value", NotNullMessage);
            }
        }

        [AssertionMethod]
        public static void IsNotNullOrEmpty([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value, string argumentName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(NotNullOrEmptyMessage, argumentName ?? "value");
            }
        }

        [AssertionMethod]
        public static void IsNotNullOrWhiteSpace([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(NotNullOrWhitespaceMessage, argumentName ?? "value");
            }
        }
    }
}