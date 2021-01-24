using Kanbersky.Beetle.Infrastructure.EventSourcing.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.Infrastructure.EventSourcing.Abstract
{
    public interface IEventStore
    {
        Task Store<TAggregate>(TAggregate aggregate, Func<StoreMessage, Task> action = null) where TAggregate : IAggregate;

        Task Store<TAggregate>(ICollection<TAggregate> aggregates, Func<StoreMessage, Task> action = null) where TAggregate : IAggregate;

        Task<TAggregate> AggregateStream<TAggregate>(Guid aggregateId, int? version = null, DateTime? createdDate = null) where TAggregate : IAggregate;

        Task<ICollection<TAggregate>> AggregateStream<TAggregate>(ICollection<Guid> ids) where TAggregate : IAggregate;

        Task AppendEvent<TAggregate>(Guid aggregateId, object @event, int? expectedVersion = null, Func<StoreMessage, Task> action = null) where TAggregate : IAggregate;

        Task<IEnumerable<StoreMessage>> GetEvents(Guid aggregateId, int? version = null, DateTime? createdDate = null);
    }
}
