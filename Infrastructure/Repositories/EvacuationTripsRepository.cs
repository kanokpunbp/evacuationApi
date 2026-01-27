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
    public class EvacuationTripsRepository : GenericRepository<EvacuationTrip>, IEvacuationTripsRepository
    {
        private readonly AppDbContext _context;


        public EvacuationTripsRepository(AppDbContext context) : base(context)
        {
            _context = context;

        }
     
        public async Task<IReadOnlyList<EvacuationTrip>> GetByPlanIdAsync(Guid planId)
        {
            return await _context.evacuationTrips
                .AsNoTracking()
                .Where(t => t.PlanId == planId)
                .OrderBy(t => t.TripSequence)
                .ToListAsync();
        }

    }
}