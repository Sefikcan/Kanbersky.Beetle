using Kanbersky.Beetle.Core.Helpers;
using Kanbersky.Beetle.Infrastructure.EventSourcing.Abstract;
using Kanbersky.Beetle.Infrastructure.EventSourcing.Entities;
using Kanbersky.Beetle.Infrastructure.EventStore.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.Infrastructure.EventSourcing.Concrete
{
    public class EventStore : IEventStore
    {
        private readonly IStore<StoreMessage> _store;

        public EventStore(IStore<StoreMessage> store)
        {
            _store = store;
        }

        public Task<TAggregate> AggregateStream<TAggregate>(Guid aggregateId, int? version = null, DateTime? createdDate = null) where TAggregate : IAggregate
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TAggregate>> AggregateStream<TAggregate>(ICollection<Guid> ids) where TAggregate : IAggregate
        {
            throw new NotImplementedException();
        }

        public async Task AppendEvent<TAggregate>(Guid aggregateId, object @event, int? expectedVersion = null, Func<StoreMessage, Task> action = null) where TAggregate : IAggregate
        {
            int version = 1;
            var events = await GetEvents(aggregateId);
            var versions = events.Select(x => x.Version).ToList();

            if (expectedVersion.HasValue)
            {
                if (versions.Contains(expectedVersion.Value))
                    throw new Exception($"Version '{expectedVersion.Value}' already exists for stream '{aggregateId}'");
            }
            else
            {
                version = versions.DefaultIfEmpty(0).Max() + 1;
            }

            var message = new StoreMessage
            {
                AggregateId = aggregateId,
                Version = version,
                Type = MessageBrokerHelper.GetTypeName(@event.GetType()),
                Data = @event == null ? "{}" : JsonConvert.SerializeObject(@event)
            };

            await _store.Add(message);

            if (action != null)
                await action(message);
        }

        public Task<IEnumerable<StoreMessage>> GetEvents(Guid aggregateId, int? version = null, DateTime? createdDate = null)
        {
            throw new NotImplementedException();
        }

        public async Task Store<TAggregate>(TAggregate aggregate, Func<StoreMessage, Task> action = null) where TAggregate : IAggregate
        {
            var events = aggregate.DequeueUncommittedEvents();
            var currentVersion = aggregate.Version - events.Count();

            foreach (var @event in events)
            {
                currentVersion++;

                await AppendEvent<TAggregate>(aggregate.Id, @event, currentVersion, action);
            }
        }

        public async Task Store<TAggregate>(ICollection<TAggregate> aggregates, Func<StoreMessage, Task> action = null) where TAggregate : IAggregate
        {
            foreach (var aggregate in aggregates)
            {
                await Store(aggregate, action);
            }
        }
    }
}
