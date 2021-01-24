using Kanbersky.Beetle.Core.Pipelines;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Kanbersky.Beetle.Core.Extensions
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelines<,>));
            services.AddHealthChecks();

            return services;
        }
    }
}
