using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;
using URF.Core.EF;
using URF.Core.EF.Trackable;


namespace EnerjiSA.GenerationService.Data
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDataServices<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
        {
            var connectionString = configuration.GetConnectionString("EnerjiSAConnection");
            services.AddDbContext<TContext>(options =>
            {
                options.UseNpgsql(connectionString).EnableSensitiveDataLogging();
            });
            services.AddScoped<DbContext, TContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(ITrackableRepository<>), typeof(TrackableRepository<>));
            return services;
        }
    }

}
