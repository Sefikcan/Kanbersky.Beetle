using Kanbersky.Beetle.Core.Settings.Concrete.EventStore;
using Kanbersky.Beetle.Core.Settings.Concrete.Infrastructure.MongoDb;
using Kanbersky.Beetle.Infrastructure.EventStore.Abstract;
using Kanbersky.Beetle.Infrastructure.EventStore.Concrete.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kanbersky.Beetle.Infrastructure.EventStore.Extensions
{
    public static class MongoDbEventStoreExtensions
    {
        public static IServiceCollection AddMongoDbEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new MongoDbSettings();
            configuration.GetSection(nameof(EventStoreSettings)).Bind(settings);
            services.Configure<MongoDbSettings>(configuration.GetSection(nameof(EventStoreSettings)));

            services.AddScoped(typeof(IStore<>), typeof(MongoDbStore<>));
            return services;
        }
    }
}
