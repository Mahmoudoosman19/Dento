using Common.Domain.Primitives;
using MediatR;

namespace Common.Application.Abstractions.Messaging
{
    public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
    {
    }
}
