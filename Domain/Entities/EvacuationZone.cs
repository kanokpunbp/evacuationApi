using evacuation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Domain.Entities
{
    public class EvacuationZone : BaseEntity
    {
        public string ZoneCode { get; set; } = null!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int NumberOfPeople { get; set; }
        public byte UrgencyLevel { get; set; }

        public ICollection<EvacuationPlan> evacuationPlans { get; set; } = null!;
    }
}
