namespace evacuation.Application.DTOs.EvacuationPlan
{
    public class UpdateEvacuationPlanDto
    {
        //public Guid PlanId { get; set; }
        public int NumberOfEvacuees { get; set; }
        public string VehicleCode { get; set; } = null!;

    }
}
 