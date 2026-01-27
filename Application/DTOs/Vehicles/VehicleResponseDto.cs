
namespace evacuation.Application.DTOs.Vehicles
{
    public class VehicleResponseDto : VehicleBaseDto
    {
        public Guid Id { get; set; }
        public string VehicleCode { get; set; } = null!;
        public string VehicleTypeName { get; set; } = null!;
        public double ETA { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
