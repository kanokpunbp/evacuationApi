using evacuation.Application.DTOs.EvacuationZones;
using evacuation.Application.Interfaces;
using evacuation.Application.Mappers;
using evacuation.Domain.Entities;
using evacuation.Domain.Interfaces;
 

namespace evacuation.Application.Services
{
    public class EvacuationZonesService : IEvacuationZoneService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEvacuationZonesRepository _evacuationZonesRepo;
        private readonly IRunningCodesRepository _runningCodesRepo;

        public EvacuationZonesService(IUnitOfWork unitOfWork,
        IEvacuationZonesRepository evacuationZonesRepo,
        IRunningCodesRepository runningCodesRepo)
        {
            _unitOfWork = unitOfWork;
            _evacuationZonesRepo = evacuationZonesRepo;
            _runningCodesRepo = runningCodesRepo;
        }


        public async Task<Guid> CreateAsync(CreateEvacuationZoneDto dto)
        {

            var running = await _runningCodesRepo.GetNextAsync("zone");
            string zoneCode = $"{running.prefix}{running.CurrentValue:D3}";

            var evacuationZone = EvacuationZoneMapper.ToEntity(dto);
            evacuationZone.Id = Guid.NewGuid();
            evacuationZone.ZoneCode = zoneCode;
            evacuationZone.CreateDate = DateTime.UtcNow;
            await _evacuationZonesRepo.AddAsync(evacuationZone);
            await _unitOfWork.SaveChangesAsync();

            return evacuationZone.Id;
        }

        public async Task<Guid> UpdateAsync(UpdateEvacuationZoneDto dto)
        {

            EvacuationZone evacuationZoneEntity = await _evacuationZonesRepo.GetByIdAsync(dto.Id) ?? throw new Exception("EvacuationZone not found");

            EvacuationZone evacuationZone = EvacuationZoneMapper.MapToEntity(evacuationZoneEntity, dto);
            evacuationZone.UpdateDate = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return evacuationZone.Id;

        }
        public async Task DeleteAsync(Guid id)
        {
            var evacuationZone = await _evacuationZonesRepo.GetByIdAsync(id) ?? throw new Exception("EvacuationZone not found");
            _evacuationZonesRepo.Delete(evacuationZone);
            await _unitOfWork.SaveChangesAsync();

        }
        public async Task<EvacuationZoneResponseDto> GetByIdAsync(Guid id)
        {

            var evacuationZone = await _evacuationZonesRepo.GetByIdAsync(id) ?? throw new Exception("EvacuationZone not found");
            return EvacuationZoneMapper.ToDto(evacuationZone);
        }
        public async Task<List<EvacuationZoneResponseDto>> GetAllAsync()
        {
            var evacuationZones = await _evacuationZonesRepo.GetAllAsync();
            return evacuationZones.Select(EvacuationZoneMapper.ToDto).ToList(); ;
        }


    }
}
