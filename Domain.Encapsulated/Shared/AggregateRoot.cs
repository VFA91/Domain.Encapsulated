namespace Domain.Encapsulated.Shared
{
    public abstract class AggregateRoot<T> : Entity<T>
       where T : ValueObject<T>
    {
    }
}
