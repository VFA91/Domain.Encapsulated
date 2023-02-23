namespace Domain.Encapsulated.Domain
{
    using global::Domain.Encapsulated.Shared;

    public sealed class Gender : ValueObject<Gender>
    {
        public static readonly int MaxLength = 1;

        public static readonly Gender Man = new("M");
        public static readonly Gender Woman = new("W");
        public static readonly Gender Unspecified = new("X");

        private readonly string _value;

        private Gender(string value)
        {
            _value = value;
        }

        public static Gender FindBy(string value)
        {
            var medal = GetAll().SingleOrDefault(s => s._value == value);

            if (medal is null)
            {
                throw new DomainException($"Possible values for {nameof(Gender)}: {string.Join(", ", GetAll().Select(s => s._value))}");
            }

            return medal;
        }

        public override string ToString() => _value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }

        private static IEnumerable<Gender> GetAll()
        {
            yield return Man;
            yield return Woman;
            yield return Unspecified;
        }
    }
}
