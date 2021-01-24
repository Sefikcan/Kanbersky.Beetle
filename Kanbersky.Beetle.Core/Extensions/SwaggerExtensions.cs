using Kanbersky.Beetle.Core.Settings.Concrete.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;

namespace Kanbersky.Beetle.Core.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerSettings = new SwaggerSettings();
            configuration.GetSection(nameof(SwaggerSettings)).Bind(swaggerSettings);
            services.Configure<SwaggerSettings>(configuration.GetSection(nameof(SwaggerSettings)));

            if (string.IsNullOrEmpty(swaggerSettings.Title))
                swaggerSettings.Title = AppDomain.CurrentDomain.FriendlyName.Trim().Trim('_');

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerSettings.Version, swaggerSettings);
                c.CustomSchemaIds(x => x.FullName);

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerSettings = new SwaggerSettings();
            configuration.GetSection(nameof(SwaggerSettings)).Bind(swaggerSettings);
            if (string.IsNullOrEmpty(swaggerSettings.Title))
                swaggerSettings.Title = AppDomain.CurrentDomain.FriendlyName.Trim().Trim('_');

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerSettings.VersionName}/swagger.json", swaggerSettings.Title);
                c.RoutePrefix = swaggerSettings.RoutePrefix;
            });

            return app;
        }
    }
}
