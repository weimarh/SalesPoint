using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial record Quantity
    {
        private const string pattern = @"^(?:[1-9]|[1-9][0-9]|100)$";
        public string Value { get; init; }
        private Quantity(string value) => Value = value;

        [GeneratedRegex(pattern)]
        private static partial Regex QuantityRegex();

        public static Quantity? Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            if (!QuantityRegex().IsMatch(value)) return null;

            return new Quantity(value);
        }
    }
}
