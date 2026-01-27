using evacuation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Application.DTOs.EvacuationPlan
{
    public class EvacuationPlanResponseDto
    {
        public string PlanId { get; set; } = string.Empty;
        public string ZoneCode { get; set; } = null!;
        public string VehicleCode { get; set; } = null!;
        public decimal ETA { get; set; }
        public int AssignedPeople { get; set; }

    }
}
