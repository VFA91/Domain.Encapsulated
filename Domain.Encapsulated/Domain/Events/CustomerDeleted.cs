namespace Domain.Encapsulated.Domain.Events
{
    using global::Domain.Encapsulated.Shared;

    public class CustomerDeleted : IDomainEvent
    {
        public int Id { get; }

        public CustomerDeleted(CustomerId id)
        {
            Id = (int)id;
        }
    }
}
