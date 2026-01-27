
using evacuation.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace evacuation.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? transaction;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            transaction = await _context.Database.BeginTransactionAsync();
        }

 
        public async Task CommitAsync(CancellationToken ct)
        {
            await _context.SaveChangesAsync(ct);
            if (transaction != null)
                await transaction.CommitAsync(ct);
        }

        public async Task RollbackAsync()
        {
            if (transaction != null)
                await transaction.RollbackAsync();
        }
    }
}
