using evacuation.Domain.Entities;
using evacuation.Domain.Interfaces;
using evacuation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Infrastructure.Repositories
{
    public class EvacuationStatusesRepository : GenericRepository<EvacuationStatus>, IEvacuationStatusesRepository
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;

        public EvacuationStatusesRepository(AppDbContext context,
        IMemoryCache cache) : base(context)
        {
            _context = context;
            _cache = cache;
        }
        public async Task<EvacuationStatus?> GetInitialStatusAsync()
        {
            //return await _context.evacuationStatuses
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(x => x.Sequence == 1);
            return await _cache.GetOrCreateAsync(
                    "evacuation_status_initial",
                    async entry =>
                    {
                        entry.Priority = CacheItemPriority.High;

                        return await _context.evacuationStatuses
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Sequence == 1);
                    });
        }

        public async Task<EvacuationStatus?> GetNextStatusAsync(Guid currentStatusId)
        {
            var current = await _context.evacuationStatuses
                .FirstOrDefaultAsync(s => s.Id == currentStatusId);

            var nextStatus = await _context.evacuationStatuses
                 .FirstOrDefaultAsync(s => s.Sequence == current.Sequence + 1);

            if (nextStatus == null) return null;

            return nextStatus;
        }


    }
}