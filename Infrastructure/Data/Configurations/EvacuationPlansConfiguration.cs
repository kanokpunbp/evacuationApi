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
    public class EvacuationPlansConfiguration : IEntityTypeConfiguration<EvacuationPlan>
    {
        public void Configure(EntityTypeBuilder<EvacuationPlan> builder)
        {

            builder.ToTable("EvacuationPlans");

            builder.HasKey(x => x.Id);

            // relationship
            builder.HasOne(x => x.Status)
                .WithMany(s => s.evacuationPlans)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Restrict);



            builder.HasOne(x => x.Zone)
              .WithMany(s => s.evacuationPlans)
              .HasForeignKey(x => x.ZoneId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Vehicle)
            .WithMany(s => s.evacuationPlans)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.Property(e => e.ETA)
               .HasPrecision(10, 2);

            //        ,[PlanCode]
            //,[ZoneId]
            //,[VehicleId]
            //,[ETA]
            //,[AssignedPeople]
            //,[Status]
            //,[CreateDate]
            //,[UpdateDate]
        }
    }
}
