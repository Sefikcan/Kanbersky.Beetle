using Kanbersky.Beetle.Core.Settings.Abstract;

namespace Kanbersky.Beetle.Core.Settings.Concrete.Outbox
{
    public class OutboxMongoDbSettings : ISettings
    {
        public string CollectionName { get; set; } = "Messages";

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; } = "OutboxDb";
    }
}
