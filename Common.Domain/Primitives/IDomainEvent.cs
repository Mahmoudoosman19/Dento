using MediatR;

namespace Common.Domain.Primitives
{
    public interface IDomainEvent : INotification
    {
        public Guid Id { get; init; }
    }
}
