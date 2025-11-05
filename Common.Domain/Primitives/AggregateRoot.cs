using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Primitives
{
    public class AggregateRoot<TKey> : Entity<TKey> where TKey : IEquatable<TKey>
    {
        private List<IDomainEvent> _domainEvents = new();

        protected AggregateRoot(TKey id)
            : base(id)
        {
        }
        protected AggregateRoot()
        {
        }

        public List<IDomainEvent> GetDomainEvents() => _domainEvents;
        public void ClearDomainEvents() => _domainEvents.Clear();
        protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
            _domainEvents.Add(domainEvent);
    }
}
