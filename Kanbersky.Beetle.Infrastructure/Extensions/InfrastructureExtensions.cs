using Kanbersky.Beetle.Core.Settings.Concrete.EventStore;
using Kanbersky.Beetle.Infrastructure.EventSourcing.Abstract;
using Kanbersky.Beetle.Infrastructure.EventSourcing.Concrete;
using Kanbersky.Beetle.Infrastructure.EventStore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Kanbersky.Beetle.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastracture(this IServiceCollection services,
            IConfiguration configuration,
            Action<DbContextOptionsBuilder> dbContextOptions = null)
        {
            #region register event store
            
            var eventStoreSettings = new EventStoreSettings();
            configuration.GetSection(nameof(EventStoreSettings)).Bind(eventStoreSettings);
            services.Configure<EventStoreSettings>(configuration.GetSection(nameof(EventStoreSettings)));

            switch (eventStoreSettings.EventStoreType)
            {
                case Core.Enums.EventStoreType.EfCore:
                    services.AddEfCoreEventStore(dbContextOptions);
                    break;
                case Core.Enums.EventStoreType.MongoDb:
                    services.AddMongoDbEventStore(configuration);
                    break;
                case Core.Enums.EventStoreType.Couchbase:
                    break;
                case Core.Enums.EventStoreType.Cassandra:
                    break;
                default:
                    throw new Exception($"Event store type '{eventStoreSettings.EventStoreType}' is not supported");
            }

            #endregion

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IEventStore, EventSourcing.Concrete.EventStore>();

            return services;
        }
    }
}
