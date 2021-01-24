using System;
using System.Collections.Generic;

namespace Kanbersky.Beetle.Infrastructure.EventSourcing.Abstract
{
    public interface IAggregate
    {
        Guid Id { get; }

        int Version { get; }

        DateTime CreatedDate { get; }

        IEnumerable<object> DequeueUncommittedEvents();
    }
}
