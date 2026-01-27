using evacuation.Domain.Entities;
using evacuation.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<EvacuationZone> evacuationZones => Set<EvacuationZone>();
        public DbSet<RunningCodes> runningCodes => Set<RunningCodes>();
        public DbSet<VehicleTypes> vehicleTypes => Set<VehicleTypes>();
        public DbSet<Vehicles> vehicles => Set<Vehicles>();
        public DbSet<EvacuationStatus> evacuationStatuses => Set<EvacuationStatus>();
        public DbSet<EvacuationPlan> evacuationPlans => Set<EvacuationPlan>();
        public DbSet<EvacuationTrip> evacuationTrips => Set<EvacuationTrip>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //ถ้าต้องการ config เพิ่ม (Fluent API) จะใส่ตรงนี้
            //เรียกใช้ Configuration ใน AppDbContext
            modelBuilder.ApplyConfiguration(new EvacuationZonesConfiguration());
            modelBuilder.ApplyConfiguration(new RunningCodesConfiguration());
            modelBuilder.ApplyConfiguration(new VehiclesConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleTypesConfiguration());
            modelBuilder.ApplyConfiguration(new EvacuationStatusesConfiguration());
            modelBuilder.ApplyConfiguration(new EvacuationPlansConfiguration());
            modelBuilder.ApplyConfiguration(new EvacuationTripsConfiguration  ());

        }
    }
}
