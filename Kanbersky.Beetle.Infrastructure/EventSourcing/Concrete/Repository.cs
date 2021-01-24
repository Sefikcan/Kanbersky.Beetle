using Kanbersky.Beetle.Infrastructure.EventSourcing.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.Infrastructure.EventSourcing.Concrete
{
    public class Repository<TAggregate> : IRepository<TAggregate> where TAggregate : IAggregate
    {
        private readonly IEventStore _eventStore;

        public Repository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task Add(TAggregate aggregate)
        {
            await _eventStore.Store(aggregate);
        }

        public async Task Add(ICollection<TAggregate> aggregates)
        {
            await _eventStore.Store(aggregates);
        }

        public async Task Delete(TAggregate aggregate)
        {
            await _eventStore.Store(aggregate);
        }

        public async Task Delete(ICollection<TAggregate> aggregates)
        {
            await _eventStore.Store(aggregates);
        }

        public async Task<TAggregate> Find(Guid id)
        {
            return await _eventStore.AggregateStream<TAggregate>(id);
        }

        public async Task<ICollection<TAggregate>> Find(ICollection<Guid> id)
        {
            return await _eventStore.AggregateStream<TAggregate>(id);
        }

        public async Task Update(TAggregate aggregate)
        {
            await _eventStore.Store(aggregate);
        }

        public async Task Update(ICollection<TAggregate> aggregates)
        {
            await _eventStore.Store(aggregates);
        }
    }
}
