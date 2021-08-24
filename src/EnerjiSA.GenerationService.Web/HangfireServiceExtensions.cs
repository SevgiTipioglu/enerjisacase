using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.PostgreSql;

namespace EnerjiSA.GenerationService.Web
{
    public static class HangfireServiceExtensions
    {
        public static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            var hangfireConnectionString = configuration.GetConnectionString("HangfireConnection");
            services.AddHangfire(x => x.UsePostgreSqlStorage(hangfireConnectionString));
            return services;
        }
    }
}
