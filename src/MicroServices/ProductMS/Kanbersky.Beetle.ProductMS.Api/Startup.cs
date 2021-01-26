using Kanbersky.Beetle.Core.Extensions;
using Kanbersky.Beetle.Infrastructure.Extensions;
using Kanbersky.Beetle.ProductMS.Services.Commands;
using Kanbersky.Beetle.ProductMS.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Kanbersky.Beetle.ProductMS.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var domain = AppDomain.CurrentDomain.GetType();

            services
                .AddInfrastracture(Configuration)
                .AddSwagger(Configuration)
                .AddCore(typeof(CreateProductCommand))
                .AddServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(Configuration);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
