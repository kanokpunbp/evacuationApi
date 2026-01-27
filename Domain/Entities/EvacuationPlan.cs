using evacuation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Domain.Entities
{
    public class EvacuationPlan : BaseEntity
    {

        public string PlanCode { get; set; } = string.Empty;
        public Guid ZoneId { get; set; }
        public Guid VehicleId { get; set; }
        public decimal ETA { get; set; }
        public int AssignedPeople { get; set; }

        public EvacuationStatus Status { get; set; } = null!;
        public Guid StatusId { get; set; }
        public bool IsActive { get; set; }

        public EvacuationZone? Zone { get; set; }
        public Vehicles? Vehicle { get; set; }

        public ICollection<EvacuationTrip> evacuationTrips { get; set; } = null!;

    }

}
