using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial record Price
    {
        private const string pattern = @"^\d+\.\d{2}$";

        private Price(string value) => Value = value;

        public string Value { get; init; }


        [GeneratedRegex(pattern)]
        private static partial Regex PriceRegex();


        public static Price? Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;


            if (!PriceRegex().IsMatch(value)) return null;


            return new Price(value);
        }
    }
}
