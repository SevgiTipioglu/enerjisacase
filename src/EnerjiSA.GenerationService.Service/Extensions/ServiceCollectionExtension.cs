using EnerjiSA.GenerationService.Service.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Service.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IPowerPlantService, PowerPlantService>();
            services.AddScoped<IPowerPlantDataService, PowerPlantDataService>();
            return services;
        }
    }
}
