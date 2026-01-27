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
    public class EvacuationStatusesConfiguration : IEntityTypeConfiguration<EvacuationStatus>
    {
        public void Configure(EntityTypeBuilder<EvacuationStatus> builder)
        {

            builder.ToTable("EvacuationStatuses");

            builder.HasKey(x => x.Id);
            // ,[StatusCode]
            //,[Description]
            //,[Sequence]
            //,[CreateDate]
            //,[UpdateDate]
        }
    }
}
