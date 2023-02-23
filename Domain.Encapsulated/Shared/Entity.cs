namespace Domain.Encapsulated.Shared
{
    using System.Collections.Generic;

    public abstract class Entity<T> : IDomainEvents
    {
        public T Id { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        public void ClearEvents() => _domainEvents.Clear();

        public override bool Equals(object obj)
        {
            var other = (Entity<T>)obj;

            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b) => !(a == b);

        public override int GetHashCode() => (GetType().ToString() + Id).GetHashCode();

        protected void AddDomainEvent(IDomainEvent newEvent) => _domainEvents.Add(newEvent);
    }
}
