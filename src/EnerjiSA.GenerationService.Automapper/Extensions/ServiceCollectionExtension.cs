using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace EnerjiSA.GenerationService.Automapper.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var assembly = Assembly.Load("EnerjiSA.GenerationService.Automapper");
            services.AddAutoMapper(assembly);

            return services;
        }
    }
}
