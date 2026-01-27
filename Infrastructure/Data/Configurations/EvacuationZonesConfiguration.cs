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
    public class EvacuationZonesConfiguration : IEntityTypeConfiguration<EvacuationZone>
    {
        public void Configure(EntityTypeBuilder<EvacuationZone> builder)
        {

            builder.ToTable("EvacuationZones");


            builder.HasKey(x => x.Id);


            builder.Property(x => x.ZoneCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasIndex(x => x.ZoneCode)
                .IsUnique();


            builder.Property(x => x.Latitude)
                .IsRequired()
                .HasPrecision(9, 6);

            builder.Property(x => x.Longitude)
                .IsRequired()
                .HasPrecision(9, 6);

            builder.Property(x => x.NumberOfPeople)
                .IsRequired();


            builder.Property(x => x.UrgencyLevel)
                .IsRequired();

            builder.Property(x => x.CreateDate)
                .IsRequired();

            builder.Property(x => x.UpdateDate)
                .IsRequired(false);
        }
    }
}
