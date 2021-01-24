using Kanbersky.Beetle.Core.Entities;
using System;

namespace Kanbersky.Beetle.Infrastructure.Outbox.Entities
{
    public class OutboxMessage : IEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public DateTime CreatedDate { get; private set; } = DateTime.Now;

        public DateTime? ProcessedDate { get; set; }

        public string Data { get; set; }

        public string Type { get; set; }
    }
}
