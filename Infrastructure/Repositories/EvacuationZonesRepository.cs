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
    public class EvacuationZonesRepository : GenericRepository<EvacuationZone>, IEvacuationZonesRepository
    {
        private readonly AppDbContext _context;


        public EvacuationZonesRepository(AppDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<List<EvacuationZone>> GetAllOrderedByUrgencyAsync()
        {
            return await _context.evacuationZones
                .AsNoTracking()
                .OrderByDescending(z => z.UrgencyLevel)
                .ToListAsync();
        }

        public async Task<EvacuationZone?> GetByZoneCodeAsync(string zoneCode)
        {
            return await _context.evacuationZones
                .AsNoTracking()
                .FirstOrDefaultAsync(z => z.ZoneCode == zoneCode);
        }

    }
}