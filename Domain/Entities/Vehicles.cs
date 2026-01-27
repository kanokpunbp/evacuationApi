using evacuation.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Domain.Entities
{
    public class Vehicles : BaseEntity
    {
        public string VehicleCode { get; set; } = null!;
        public int Capacity { get; set; }
        public Guid VehicleTypeId { get; set; }
        public decimal Speed { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool Status { get; set; }

        public VehicleTypes VehicleType { get; set; }

        public ICollection<EvacuationPlan> evacuationPlans { get; set; } = null!;

    }
}
