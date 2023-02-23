namespace Domain.Encapsulated.Domain
{
    using global::Domain.Encapsulated.Shared;
    using global::Domain.Encapsulated.Validations;
    using System.Collections.Generic;

    public sealed class Code : ValueObject<Code>
    {
        public const int MaxLength = 6;

        private readonly string _value;

        public Code(string value)
        {
            Ensure.Argument.NotNull(value, nameof(value));

            _value = value
                .ToUpperInvariant()
                .IsNotWhiteSpace()
                .HasMaxLength(MaxLength)
                .ContainsOnly(CharRanges.AlphanumericUpperCase);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }
    }
}
