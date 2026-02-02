using Microsoft.OpenApi.Models;

namespace UrlShortnerAPI
{
    internal static class SwaggerExtensions
    {
        internal static void ConfigureSwaggerServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                foreach (var name in Directory.GetFiles(AppContext.BaseDirectory, "*.XML", SearchOption.AllDirectories))
                {
                    c.IncludeXmlComments(filePath: name);
                }

                c.TagActionsBy(d => [d.HttpMethod]);

                c.SwaggerDoc(
                    UrlShortnerRouteMapping.UrlShortnerApi,
                    new OpenApiInfo { Title = "Url Shortner Service Route Mapping", Version = UrlShortnerRouteMapping.UrlShortnerApi });

            });
        }

        internal static void ConfigureSwagger(this WebApplication app)
        {
            app.UseSwagger(opt => opt.RouteTemplate = "/swagger/{documentName}/swagger.json");
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{UrlShortnerRouteMapping.UrlShortnerApi}/swagger.json",
            UrlShortnerRouteMapping.UrlShortnerApi));
        }
    }
}
