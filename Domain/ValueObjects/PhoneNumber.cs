using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial record PhoneNumber
    {
        private const int DefaultLenght = 8;
        private const string Pattern = @"^\d+$";

        public string Value { get; init; }

        private PhoneNumber(string value) => Value = value;

        public static PhoneNumber? Create(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;

            if (value.Length != DefaultLenght)
                return null;

            if (!PhoneNumberRegex().IsMatch(value))
                return null;

            return new PhoneNumber(value);
        }

        [GeneratedRegex(Pattern)]
        private static partial Regex PhoneNumberRegex();
    }
}
