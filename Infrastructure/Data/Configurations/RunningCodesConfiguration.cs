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
    public class RunningCodesConfiguration : IEntityTypeConfiguration<RunningCodes>
    {
        public void Configure(EntityTypeBuilder<RunningCodes> builder) {
            
            builder.ToTable("RunningCodes");
    
            builder.HasKey(x => x.Name);
               
        }
    }
}
       