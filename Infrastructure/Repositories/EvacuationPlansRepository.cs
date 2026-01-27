using evacuation.Application.DTOs.EvacuationPlan;
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
    public class EvacuationPlansRepository : GenericRepository<EvacuationPlan>, IEvacuationPlansRepository
    {
        private readonly AppDbContext _context;


        public EvacuationPlansRepository(AppDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<EvacuationPlan?> GetByZoneIdAsync(Guid zoneId)
        {

            return await _context.evacuationPlans
                .AsNoTracking()
                .FirstOrDefaultAsync(z => z.ZoneId == zoneId);
        }

        public async Task ClearAllActiveAsync()
        {
            var plans = await _context.evacuationPlans.ToListAsync();
            await _context.SaveChangesAsync();
        }

        public async Task SetInActiveAsync()
        {
            var plans = await _context.evacuationPlans
                .Where(p => p.IsActive == true)
                .ToListAsync();

            foreach (var plan in plans)
            {
                plan.IsActive = false;
                plan.UpdateDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
        public async Task<IReadOnlyList<EvacuationPlanResponseDto>> GetPlanActiveAsync()
        {
            return await _context.evacuationPlans
                .AsNoTracking()
                .Where(p => p.IsActive)
                .Select(p => new EvacuationPlanResponseDto
                {
                    PlanId = p.Id.ToString(),
                    ZoneCode = p.Zone != null ? p.Zone.ZoneCode : string.Empty,
                    VehicleCode = p.Vehicle != null ? p.Vehicle.VehicleCode : string.Empty,
                    ETA = p.ETA,
                    AssignedPeople = p.AssignedPeople
                })
                .ToListAsync();
        }

        public async Task<IReadOnlyList<EvacuationPlan>> GetActiveAsync()
        {
            return await _context.evacuationPlans
                .AsNoTracking()
                .Include(p => p.Zone)
                .Include(p => p.Vehicle)
                .Where(p => p.IsActive)
                .ToListAsync();
        }

    }
}