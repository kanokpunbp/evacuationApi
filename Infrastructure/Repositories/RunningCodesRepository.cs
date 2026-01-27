using evacuation.Domain.Entities;
using evacuation.Domain.Interfaces;
using evacuation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace evacuation.Infrastructure.Repositories
{
    public class RunningCodesRepository : GenericRepository<RunningCodes>, IRunningCodesRepository
    {
        private readonly AppDbContext _context;


        public RunningCodesRepository(AppDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<RunningCodes> GetNextAsync(string name)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            try
            {
                RunningCodes obj = await _context.runningCodes.Where(r => r.Name == name).SingleAsync();
                obj.CurrentValue = obj.CurrentValue + 1;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return obj;
            }
            catch  
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


    }
}