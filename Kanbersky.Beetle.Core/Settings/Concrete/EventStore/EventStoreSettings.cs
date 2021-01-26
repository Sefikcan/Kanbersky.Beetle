using Kanbersky.Beetle.Core.Enums;
using Kanbersky.Beetle.Core.Settings.Abstract;

namespace Kanbersky.Beetle.Core.Settings.Concrete.EventStore
{
    public class EventStoreSettings : ISettings
    {
        public EventStoreType EventStoreType { get; set; }
    }
}
