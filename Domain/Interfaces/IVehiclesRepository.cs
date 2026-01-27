using evacuation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Domain.Interfaces
{
    public interface IVehiclesRepository : IGenericRepository<Vehicles>
    {

        Task<Vehicles> GetVehicle(Guid id);
        Task<List<Vehicles>> GetAllVehicles();
        Task<List<Vehicles>> GetAvailableVehiclesAsync();
        Task<Vehicles?> GetByVehicleCodeAsync(string vehicleCode);
    }
}


