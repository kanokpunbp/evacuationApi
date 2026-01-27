using evacuation.Domain.Entities;

namespace evacuation.Domain.Interfaces
{
    public interface IEvacuationPlansRepository : IGenericRepository<EvacuationPlan>
    {
        Task<EvacuationPlan?> GetByZoneIdAsync(Guid zoneId);
        Task SetInActiveAsync();
        //Task<IReadOnlyList<EvacuationPlanResponseDto>> GetPlanActiveAsync();
        Task<IReadOnlyList<EvacuationPlan>> GetActiveAsync();
    }
}
