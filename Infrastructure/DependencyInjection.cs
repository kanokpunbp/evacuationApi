using evacuation.Application.Interfaces;
using evacuation.Domain.Interfaces;
using evacuation.Infrastructure.Data;
using evacuation.Infrastructure.Redis;
using evacuation.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEvacuationZonesRepository, EvacuationZonesRepository>();
            services.AddScoped<IRunningCodesRepository, RunningCodesRepository>();
            services.AddScoped<IVehiclesRepository, VehiclesRepository>();
            services.AddScoped<IEvacuationPlansRepository, EvacuationPlansRepository>();
            services.AddScoped<IEvacuationTripsRepository, EvacuationTripsRepository>();
            services.AddScoped<IEvacuationStatusesRepository, EvacuationStatusesRepository>();


            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();

                var redisConnection = configuration["Redis:ConnectionString"];

                var options = ConfigurationOptions.Parse(redisConnection, true);
                options.AbortOnConnectFail = false;

                return ConnectionMultiplexer.Connect(options);
            });


            services.AddScoped<IEvacuationStatusCache, EvacuationStatusRedisCache>();

            return services;
        }
    }
}
