using evacuation.Application.DTOs.EvacuationPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Application.Interfaces
{
    public interface IEvacuationStatusCache
    {
        Task InitializeZoneAsync(string planId, string zoneCode, int totalPeople);

        Task UpdateAsync(string zoneCode, int evacuatedDelta, string? lastVehicle);

        Task<IReadOnlyList<EvacuationStatusDto>> GetAllAsync();

        Task ClearAsync();
    }
}
