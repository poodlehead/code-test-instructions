namespace UrlShortnerAPI
{
    internal static class LogBuilderExtensions
    {
        internal static void ConfigureLogging(this ILoggingBuilder builder)
        {
            builder
                .ClearProviders()
                .AddJsonConsole();
        }
    }
}
