
namespace evacuation.Application.DTOs.EvacuationZones
{
    public abstract class EvacuationZoneBaseDto
    {
        //public string ZoneCode { get; set; } = null!;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public int NumberOfPeople { get; set; }

        public byte UrgencyLevel { get; set; }
    }
}

