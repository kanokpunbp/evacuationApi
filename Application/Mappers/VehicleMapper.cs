using evacuation.Application.DTOs.Vehicles;
using evacuation.Domain.Entities;

namespace evacuation.Application.Mappers
{
    public static class VehicleMapper
    {
        public static Vehicles ToEntity(CreateVehicleDto dto)
        {
            return new Vehicles
            {
                Capacity = dto.Capacity,
                VehicleTypeId = dto.VehicleTypeId,
                Speed = dto.Speed,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude
            };
        }

        public static Vehicles MapToEntity(Vehicles entity, UpdateVehicleDto dto)
        {

            entity.Capacity = dto.Capacity;
            entity.VehicleTypeId = dto.VehicleTypeId;
            entity.Speed = dto.Speed;
            entity.Latitude = dto.Latitude;
            entity.Longitude = dto.Longitude;
            return entity;
        }

        public static VehicleResponseDto ToDto(Vehicles entity)
        {
            return new VehicleResponseDto
            {
                Id = entity.Id,
                VehicleCode = entity.VehicleCode,
                VehicleTypeName = entity.VehicleType.TypeName,
                Capacity = entity.Capacity,
                VehicleTypeId = entity.VehicleTypeId,
                Speed = entity.Speed,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate
            };
        }
    }
}
 
 