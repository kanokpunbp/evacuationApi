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
    public class VehicleTypesConfiguration : IEntityTypeConfiguration<VehicleTypes>
    {
        public void Configure(EntityTypeBuilder<VehicleTypes> builder)
        {

            builder.ToTable("VehicleTypes");

            builder.HasKey(x => x.Id);

            builder.HasData(
                  new VehicleTypes
                  {
                      Id = Guid.Parse("8E637482-A0C2-45A2-B97E-0C8C87E1B120"),
                      TypeName = "bus",
                      CreateDate = new DateTime(2026, 1, 22, 15, 1, 53),
                      UpdateDate = null
                  },
                  new VehicleTypes
                  {
                      Id = Guid.Parse("DDF12D9D-C3E0-460B-9730-145E2016C27D"),
                      TypeName = "van",
                      CreateDate = new DateTime(2026, 1, 22, 15, 1, 57),
                      UpdateDate = null
                  },
                  new VehicleTypes
                  {
                      Id = Guid.Parse("BBBD36D6-3B89-4FDE-927C-0CDDF4D1F9F1"),
                      TypeName = "boat",
                      CreateDate = new DateTime(2026, 1, 22, 15, 2, 1),
                      UpdateDate = null
                  }
              );

        }
    }
}
