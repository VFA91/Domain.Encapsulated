namespace Domain.Encapsulated.Validations
{
    using global::Domain.Encapsulated.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Resources = Shared.Validations.Resources;

    public static class StringExtension
    {
        private static readonly Dictionary<CharRanges, string> _charRanges = new()
            {
                { CharRanges.LowerCaseLetters, "abcdefghijklmnopqrstuvwxyz" },
                { CharRanges.UpperCaseLetters, "ABCDEFGHIJKLMNOPQRSTUVWXYZ" },
                { CharRanges.Digits, "0123456789" },
                { CharRanges.Spaces, " " },
                { CharRanges.Dash, "-" },
                { CharRanges.Dot, "." },
                { CharRanges.Slash, "/" },
            };

        public static string ContainsOnly(this string value, CharRanges charRange)
        {
            var validCharsBuilder = new StringBuilder();

            foreach (var item in _charRanges.Where(item => charRange.HasFlag(item.Key)))
            {
                validCharsBuilder.Append(item.Value);
            }

            return ContainsOnly(value, validCharsBuilder.ToString());
        }

        public static string ContainsOnly(this string value, IEnumerable<char> validChars)
        {
            if (validChars is null)
            {
                throw new ArgumentNullException(nameof(validChars));
            }

            if (value.Any(c => !validChars.Contains(c)))
            {
                throw new DomainException(string.Format(Resources.CharRanges, value));
            }

            return value;
        }

        public static string DoesNotContain(this string value, IEnumerable<char> invalidChars)
        {
            if (invalidChars is null)
            {
                throw new ArgumentNullException(nameof(invalidChars));
            }

            if (value.Any(c => invalidChars.Contains(c)))
            {
                throw new DomainException(string.Format(Resources.CharRanges, value));
            }

            return value;
        }

        public static string IsNotNull(this string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), value);
            }

            return value;
        }

        public static string IsNotEmpty(this string value)
        {
            value.IsNotNull();

            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException(Resources.NotEmpty);
            }

            return value;
        }

        public static string IsNotWhiteSpace(this string value)
        {
            value.IsNotEmpty();

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException(Resources.NotWhiteSpace);
            }

            return value;
        }

        public static string HasMaxLength(this string value, int maxValue)
        {
            if (value.Length > maxValue)
            {
                throw new DomainException(string.Format(Resources.MaxLength, maxValue));
            }

            return value;
        }

        public static string HasEqualsLength(this string value, int length)
        {
            if (value.Length != length)
            {
                throw new DomainException(string.Format(Resources.EqualsLength, length));
            }

            return value;
        }

        public static string RegexMatch(this string value, string regexPattern)
        {
            var regex = new Regex(regexPattern);

            var match = regex.Match(value);

            if (!match.Success)
            {
                throw new DomainException(Resources.RegexMatch);
            }

            return value;
        }
    }

    [Flags]
    public enum CharRanges
    {
        LowerCaseLetters = 1,
        UpperCaseLetters = 2,
        Digits = 4,
        Spaces = 8,
        Dash = 16,
        Dot = 32,
        Slash = 64,
        Letters = LowerCaseLetters | UpperCaseLetters,
        Alphanumeric = LowerCaseLetters | UpperCaseLetters | Digits,
        AlphanumericLowerCase = LowerCaseLetters | Digits,
        AlphanumericUpperCase = UpperCaseLetters | Digits,
    }
}
