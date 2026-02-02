using System.Text.Json;
using System.Text.Json.Serialization;

namespace UrlShortnerAPI
{
    internal static class JsonOptionsExtensions
    {
        internal static IServiceCollection ConfigureJsonOptions(this IServiceCollection services)
        {
            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(static options => ConfigureJsonSerializerOptions(options.SerializerOptions));
            services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(static options => ConfigureJsonSerializerOptions(options.JsonSerializerOptions));
            return services;
        }

        private static void ConfigureJsonSerializerOptions(JsonSerializerOptions serializerOptions)
        {
            serializerOptions.Converters.Add(new JsonStringEnumConverter());
            serializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            serializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        }
    }
}
