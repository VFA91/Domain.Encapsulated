namespace Domain.Encapsulated.Domain.Events
{
    using global::Domain.Encapsulated.Shared;

    public sealed class CustomerCreated : IDomainEvent
    {
        public int Id { get; }
        public string Code { get; }
        public string Name { get; }
        public string Gender { get; }

        public CustomerCreated(CustomerId id, Code code, Name name, Gender gender)
        {
            Id = (int)id;
            Code = code.ToString();
            Name = name.ToString();
            Gender = gender.ToString();
        }
    }
}
