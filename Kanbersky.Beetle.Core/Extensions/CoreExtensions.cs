using FluentValidation.AspNetCore;
using Kanbersky.Beetle.Core.Pipelines;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Kanbersky.Beetle.Core.Extensions
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, params Type[] types)
        {
            var assemblies = types.Select(type => type.GetTypeInfo().Assembly);

            foreach (var assembly in assemblies)
            {
                services.AddMediatR(assembly);
            }

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelines<,>));
            services.AddHealthChecks();

            services.AddControllers()
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblies(assemblies); });

            return services;
        }
    }
}
