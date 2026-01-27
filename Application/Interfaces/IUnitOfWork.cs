namespace evacuation.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync(CancellationToken ct);
        Task RollbackAsync();
    }

}
