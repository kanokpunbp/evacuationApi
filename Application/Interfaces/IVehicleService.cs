using evacuation.Application.DTOs.Vehicles;

namespace evacuation.Application.Interfaces
{
    public interface IVehicleService
    {
        Task<Guid> CreateAsync(CreateVehicleDto dto);
        Task<Guid> UpdateAsync(UpdateVehicleDto dto);
        Task DeleteAsync(Guid id);
        Task<VehicleResponseDto> GetByIdAsync(Guid id);
        Task<List<VehicleResponseDto>> GetAllAsync();
    }
}
