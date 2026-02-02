using BusinessLogic.VisitorModel.CreateUrl;
using BusinessLogic.VisitorModel.GetAllUrl;
using BusinessLogic.VisitorModel.GetUrl;

namespace UrlShortnerAPI
{
    public static class ResultVisitorExtensions
    {
        public static IServiceCollection AddResultVisitorServices(this IServiceCollection services)
        { 
            services.AddTransient<IUrlShortnerCreateResultVisitor<IResult>, UrlShortnerCreateResultVisitor>();
            services.AddTransient<IUrlShortnerGetResultVisitor<IResult>, UrlShortnerGetResultVisitor>();
            services.AddTransient<IUrlShortnerGetAllResultVisitor<IResult>, UrlShortnerGetAllResultVisitor>();

            return services;
        }
    }
}
