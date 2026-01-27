using evacuation.Application.DTOs.EvacuationZones;
using evacuation.Domain.Entities;

namespace evacuation.Application.Mappers
{
    public static class EvacuationZoneMapper
    {
        public static EvacuationZone ToEntity(CreateEvacuationZoneDto dto)
        {
            return new EvacuationZone
            {
                //ZoneCode = dto.ZoneCode,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                NumberOfPeople = dto.NumberOfPeople,
                UrgencyLevel = dto.UrgencyLevel
            };
        }

        public static EvacuationZone MapToEntity(
            EvacuationZone entity,
            UpdateEvacuationZoneDto dto)
        {
            //entity.ZoneCode = dto.ZoneCode;
            entity.Latitude = dto.Latitude;
            entity.Longitude = dto.Longitude;
            entity.NumberOfPeople = dto.NumberOfPeople;
            entity.UrgencyLevel = dto.UrgencyLevel;
            return entity;
        }

        public static EvacuationZoneResponseDto ToDto(EvacuationZone entity)
        {
            return new EvacuationZoneResponseDto
            {
                Id = entity.Id,
                ZoneCode = entity.ZoneCode,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                NumberOfPeople = entity.NumberOfPeople,
                UrgencyLevel = entity.UrgencyLevel,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate
            };
        }
    }
}
