using evacuation.Application.Interfaces;
using evacuation.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace evacuation.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<IEvacuationZoneService, EvacuationZonesService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IEvacuationPlanService, EvacuationPlanService>();

            return services;
        }
    }
}
