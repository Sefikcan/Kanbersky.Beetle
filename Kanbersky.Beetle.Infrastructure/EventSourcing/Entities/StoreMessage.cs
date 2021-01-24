using Kanbersky.Beetle.Core.Entities;
using System;

namespace Kanbersky.Beetle.Infrastructure.EventSourcing.Entities
{
    public class StoreMessage : IEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid AggregateId { get; set; }

        public DateTime CreatedDate { get; private set; } = DateTime.Now;

        public string Type { get; set; }

        public string Data { get; set; }

        public int Version { get; set; } = 0;
    }
}
