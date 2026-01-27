
namespace evacuation.Application.DTOs.Vehicles
{
    public abstract class VehicleBaseDto
    {
        public int Capacity { get; set; }
        public Guid VehicleTypeId { get; set; }

        public decimal Speed { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }


    }
}



