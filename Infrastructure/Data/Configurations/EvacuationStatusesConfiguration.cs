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

            builder.HasData(
               new EvacuationStatus
               {
                   Id = Guid.Parse("4464588D-BD4F-4AD9-BA60-217B8B163E61"),
                   StatusCode = "READY",
                   Description = "พร้อมดำเนินการ",
                   Sequence = 1,
                   CreateDate = new DateTime(2026, 1, 22, 11, 52, 14),
                   UpdateDate = null
               },
               new EvacuationStatus
               {
                   Id = Guid.Parse("423C1CCB-FCFF-44F6-A9C5-4E5BCB84F5F6"),
                   StatusCode = "INPROGRESS",
                   Description = "กำลังดำเนินการ",
                   Sequence = 2,
                   CreateDate = new DateTime(2026, 1, 22, 11, 52, 14),
                   UpdateDate = null
               },
               new EvacuationStatus
               {
                   Id = Guid.Parse("56C51499-C1D3-411A-B7E4-37D43A6E0BDA"),
                   StatusCode = "COMPLETED",
                   Description = "เสร็จสิ้น",
                   Sequence = 3,
                   CreateDate = new DateTime(2026, 1, 22, 11, 52, 14),
                   UpdateDate = null
               }
           );
        }
    }
}
