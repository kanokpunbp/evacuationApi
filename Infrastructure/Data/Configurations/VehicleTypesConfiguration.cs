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
        public void Configure(EntityTypeBuilder<VehicleTypes> builder) {
            
            builder.ToTable("VehicleTypes");

           
            builder.HasKey(x => x.Id);     
        
        }
    }
}
     