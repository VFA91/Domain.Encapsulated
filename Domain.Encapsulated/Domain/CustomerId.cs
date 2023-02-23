namespace Domain.Encapsulated.Domain
{
    using global::Domain.Encapsulated.Shared;
    using System;
    using System.Collections.Generic;

    public sealed class CustomerId : ComparableValueObject<CustomerId>
    {
        private readonly int _value;

        public CustomerId(int value)
        {
            _value = value;
        }

        private CustomerId()
        {
        }

        public static explicit operator CustomerId(int id) => new(id);

        public static explicit operator int(CustomerId id) => id._value;

        protected override IEnumerable<IComparable> GetComparisonComponents()
        {
            yield return _value;
        }
    }
}
