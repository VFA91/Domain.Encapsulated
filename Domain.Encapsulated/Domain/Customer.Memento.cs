namespace Domain.Encapsulated.Domain
{
    using global::Domain.Encapsulated.Shared;

    public partial class Customer : AggregateRoot<CustomerId>
    {
        public CustomerMemento CreateMemento() => new()
        {
            Id = (int)Id,
            Code = _code.ToString(),
            Name = _name.ToString(),
            Gender = _gender.ToString()
        };

        public static Customer FromMemento(CustomerMemento customerMemento) =>
            new(
                new CustomerId(customerMemento.Id),
                new Code(customerMemento.Code),
                new Name(customerMemento.Name),
                Gender.FindBy(customerMemento.Gender));
    }

    public class CustomerMemento
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }
}
