using evacuation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Infrastructure.Data.Configurations
{
    public class EvacuationTripsConfiguration : IEntityTypeConfiguration<EvacuationTrip>
    {
        public void Configure(EntityTypeBuilder<EvacuationTrip> builder)
        {

            builder.ToTable("EvacuationTrips");

            builder.HasKey(x => x.Id);


            // relationship
            builder.HasOne(t => t.evacuationPlan)
                .WithMany(p => p.evacuationTrips)
                .HasForeignKey(t => t.PlanId)
                .OnDelete(DeleteBehavior.Restrict);


            //    ,[PlanId]
            //,[VehicleId]
            //,[PeopleCount]
            //,[TripSequence]
            //,[StartTime]
            //,[EndTime]
            //,[CreateDate]
            //,[UpdateDate]
        }
    }
}
