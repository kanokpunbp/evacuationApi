using evacuation.Domain.Entities;
using evacuation.Domain.Interfaces;
using evacuation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Infrastructure.Repositories
{
    public class VehiclesRepository : GenericRepository<Vehicles>, IVehiclesRepository
    {
        private readonly AppDbContext _context;


        public VehiclesRepository(AppDbContext context) : base(context)
        {
            _context = context;

        }
        public async Task<Vehicles> GetVehicle(Guid id)
        {
            var vehicle = await _context.vehicles
                            .Include(v => v.VehicleType)
                            .FirstOrDefaultAsync(v => v.Id == id);

            return vehicle;
        }
        public async Task<List<Vehicles>> GetAllVehicles()
        {

            var vehicles = await _context.vehicles
                            .Include(v => v.VehicleType)
                            .ToListAsync();

            return vehicles;
        }
        public async Task<List<Vehicles>> GetAvailableVehiclesAsync()
        {
            return await _context.vehicles
                .AsNoTracking()
                .Where(v => v.Status == true)
                .ToListAsync();
        }

        public async Task<Vehicles?> GetByVehicleCodeAsync(string vehicleCode)
        {

            return await _context.vehicles
                .AsNoTracking()
                .FirstOrDefaultAsync(z => z.VehicleCode == vehicleCode);
        }

    }
}