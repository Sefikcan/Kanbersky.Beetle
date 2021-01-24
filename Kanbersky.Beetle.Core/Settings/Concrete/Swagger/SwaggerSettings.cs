using Kanbersky.Beetle.Core.Settings.Abstract;
using Microsoft.OpenApi.Models;

namespace Kanbersky.Beetle.Core.Settings.Concrete.Swagger
{
    public class SwaggerSettings : OpenApiInfo, ISettings
    {
        public string VersionName { get; set; } = "v1";

        public string RoutePrefix { get; set; } = "";
    }
}
