using Kanbersky.Beetle.Core.Settings.Abstract;

namespace Kanbersky.Beetle.Core.Settings.Concrete.Infrastructure.MongoDb
{
    public class MongoDbSettings : ISettings
    {
        public string CollectionName { get; set; }

        public string ConnectionStrings { get; set; }

        public string DatabaseName { get; set; }
    }
}
