namespace Domain.Encapsulated.Domain
{
    using global::Domain.Encapsulated.Shared;
    using global::Domain.Encapsulated.Validations;
    using System;
    using System.Collections.Generic;

    public sealed class Name : ComparableValueObject<Name>
    {
        public const int MaxLength = 50;

        private readonly string _value;

        public Name(string value)
        {
            Ensure.Argument.NotNull(value, nameof(value));

            _value = value
               .Trim()
               .IsNotWhiteSpace()
               .HasMaxLength(MaxLength);
        }

        protected override IEnumerable<IComparable> GetComparisonComponents()
        {
            yield return _value;
        }
    }
}
