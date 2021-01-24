using System;

namespace Kanbersky.Beetle.Core.MessageBrokers.Models
{
    public abstract class EventModel
    {
        public Guid QueueId { get; set; }

        public DateTime CreatedDate { get; set; }

        public EventModel()
        {
            QueueId = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public EventModel(Guid queueId, DateTime createdDate)
        {
            QueueId = queueId;
            CreatedDate = createdDate;
        }
    }
}
