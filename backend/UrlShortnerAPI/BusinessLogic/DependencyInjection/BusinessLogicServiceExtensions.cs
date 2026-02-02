using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DependencyInjection
{
    public static class BusinessLogicServiceExtensions
    {
        public static IServiceCollection AddBusinessLogicServices(
            this IServiceCollection services)
        { 
            services.AddTransient<IUrlShortnerService, UrlShortnerService>();

            return services;
        }
    }
}
