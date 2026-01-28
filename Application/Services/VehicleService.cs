using evacuation.Application.DTOs.Vehicles;
using evacuation.Application.Interfaces;
using evacuation.Application.Mappers;
using evacuation.Domain.Interfaces;

namespace evacuation.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehiclesRepository _vehiclesRepo;
        private readonly IRunningCodesRepository _runningCodesRepo;

        public VehicleService(IUnitOfWork unitOfWork,
        IVehiclesRepository vehiclesRepo,
        IRunningCodesRepository runningCodesRepo)
        {
            _unitOfWork = unitOfWork;
            _vehiclesRepo = vehiclesRepo;
            _runningCodesRepo = runningCodesRepo;
        }


        public async Task<Guid> CreateAsync(CreateVehicleDto dto)
        {

            var running = await _runningCodesRepo.GetNextAsync("vehicle");
            string vehicleCode = $"{running.Prefix}{running.CurrentValue:D3}";

            var vehicle = VehicleMapper.ToEntity(dto);
            vehicle.Id = Guid.NewGuid();
            vehicle.VehicleCode = vehicleCode;
            vehicle.CreateDate = DateTime.UtcNow;
            vehicle.Status = true;
            await _vehiclesRepo.AddAsync(vehicle);
            await _unitOfWork.SaveChangesAsync();

            return vehicle.Id;
        }

        public async Task<Guid> UpdateAsync(UpdateVehicleDto dto)
        {

            var vehicleEntity = await _vehiclesRepo.GetByIdAsync(dto.Id) ?? throw new Exception("Vehicle not found");

            var vehicle = VehicleMapper.MapToEntity(vehicleEntity, dto);
            vehicle.UpdateDate = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return vehicle.Id;

        }
        public async Task DeleteAsync(Guid id)
        {
            var Vehicle = await _vehiclesRepo.GetByIdAsync(id) ?? throw new Exception("Vehicle not found");
            _vehiclesRepo.Delete(Vehicle);
            await _unitOfWork.SaveChangesAsync();

        }
        public async Task<VehicleResponseDto> GetByIdAsync(Guid id)
        {

            var vehicle = await _vehiclesRepo.GetVehicle(id) ?? throw new Exception("Vehicle not found");  
               
            return VehicleMapper.ToDto(vehicle);
        }
        public async Task<List<VehicleResponseDto>> GetAllAsync()
        {
            var vehicles = await _vehiclesRepo.GetAllVehicles();
            return vehicles.Select(VehicleMapper.ToDto).ToList() ;
        }


    }
}
