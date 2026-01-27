
namespace evacuation.Application.DTOs.Vehicles
{
    public class UpdateVehicleDto : VehicleBaseDto
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
    }
}
