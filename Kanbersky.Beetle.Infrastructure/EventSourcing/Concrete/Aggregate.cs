using Kanbersky.Beetle.Infrastructure.EventSourcing.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kanbersky.Beetle.Infrastructure.EventSourcing.Concrete
{
    public class Aggregate : IAggregate
    {
        public Guid Id { get; protected set; }

        public int Version { get; protected set; } = 0;

        public DateTime CreatedDate { get; protected set; } = DateTime.Now;

        public Aggregate() { }

        protected IList<object> UncommittedEvents = new List<object>();

        public IList<object> Apply(object @event)
        {
            UncommittedEvents.Add(@event);
            return UncommittedEvents;
        }

        public IEnumerable<object> DequeueUncommittedEvents()
        {
            var dequeudEvents = UncommittedEvents.ToList();
            UncommittedEvents.Clear();
            return dequeudEvents;
        }
    }
}
