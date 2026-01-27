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
    public class VehiclesConfiguration : IEntityTypeConfiguration<Vehicles>
    {
        public void Configure(EntityTypeBuilder<Vehicles> builder)
        {

            builder.ToTable("Vehicles");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Latitude)
                .HasPrecision(9, 6);

            builder.Property(e => e.Longitude)
                .HasPrecision(9, 6);

            builder.Property(e => e.Speed)
                .HasPrecision(6, 2);

        }
    }
}