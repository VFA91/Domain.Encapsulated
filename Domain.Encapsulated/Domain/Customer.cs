namespace Domain.Encapsulated.Domain
{
    using global::Domain.Encapsulated.Domain.Events;
    using global::Domain.Encapsulated.Shared;

    public partial class Customer : AggregateRoot<CustomerId>
    {
        private Code _code;
        private Name _name;
        private Gender _gender;

        private Customer(CustomerId id, Code code, Name name, Gender gender)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));

            SetCode(code);
            SetName(name);
            SetGender(gender);
        }

        public static Customer Create(CustomerId id, Code code, Name name, Gender gender)
        {
            var customer = new Customer(id, code, name, gender);

            customer.AddDomainEvent(new CustomerCreated(customer.Id, customer._code, customer._name, customer._gender));

            return customer;
        }

        public void Update(Code code, Name name, Gender gender)
        {
            SetCode(code);
            SetName(name);
            SetGender(gender);

            AddDomainEvent(new CustomerUpdated(Id, _code, _name, _gender));
        }

        public void Delete() => AddDomainEvent(new CustomerDeleted(Id));

        private void SetCode(Code code) => _code = code ?? throw new ArgumentNullException(nameof(code));
        private void SetName(Name name) => _name = name ?? throw new ArgumentNullException(nameof(name));
        private void SetGender(Gender gender) => _gender = gender ?? throw new ArgumentNullException(nameof(gender));
    }
}
