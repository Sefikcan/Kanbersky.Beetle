using Kanbersky.Beetle.Infrastructure.EventStore.Abstract;
using Kanbersky.Beetle.Infrastructure.EventStore.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Kanbersky.Beetle.Infrastructure.EventStore.Extensions
{
    public static class EfCoreEventStoreExtensions
    {
        public static IServiceCollection AddEfCoreEventStore(this IServiceCollection services,
            Action<DbContextOptionsBuilder> dbContextOptions)
        {
            services.AddDbContext<EventStoreEfDbContext>(dbContextOptions);
            services.AddScoped(typeof(IStore<>), typeof(EfEventStore<>));

            return services;
        }
    }
}
