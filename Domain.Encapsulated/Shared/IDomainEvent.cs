namespace Domain.Encapsulated.Shared
{
    using MediatR;
    using System.Collections.Generic;

    public interface IDomainEvent : INotification
    {
    }

    public interface IDomainEvents
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }

        void ClearEvents();
    }
}
