using evacuation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Domain.Entities
{
    public class EvacuationTrip : BaseEntity
    {

        public Guid PlanId { get; set; }
        public Guid? VehicleId { get; set; }
        public int PeopleCount { get; set; }
        public int TripSequence { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public EvacuationPlan evacuationPlan { get; set; } = null!;
    }

}

