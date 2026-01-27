
namespace evacuation.Application.DTOs.EvacuationZones
{
    public class EvacuationZoneResponseDto : EvacuationZoneBaseDto
    {
        public Guid Id { get; set; }
        public string ZoneCode { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
