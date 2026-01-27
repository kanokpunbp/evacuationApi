using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Application.DTOs.EvacuationPlan
{
    public class EvacuationStatusDto
    {
        public string PlanId { get; set; } = string.Empty;
        public string ZoneCode { get; set; } = null!;

        public int TotalPeople { get; set; }

        public int Evacuated { get; set; }

        public int Remaining { get; set; }

        public string? LastVehicleUsed { get; set; }
    }
}
