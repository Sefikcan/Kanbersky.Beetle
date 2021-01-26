using Kanbersky.Beetle.ProductMS.Services.Aggregate;
using Kanbersky.Beetle.ProductMS.Services.Aggregate.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Kanbersky.Beetle.ProductMS.Services.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductAggregate, ProductAggregate>();

            return services;
        }
    }
}
