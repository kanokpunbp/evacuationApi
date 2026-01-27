using evacuation.Application.DTOs.EvacuationZones;

namespace evacuation.Application.Interfaces
{
    public interface IEvacuationZoneService
    {
        Task<Guid> CreateAsync(CreateEvacuationZoneDto dto);
        Task<Guid> UpdateAsync(UpdateEvacuationZoneDto dto);
        Task DeleteAsync(Guid id);
        Task<EvacuationZoneResponseDto> GetByIdAsync(Guid id);
        Task<List<EvacuationZoneResponseDto>> GetAllAsync();
    }
}
