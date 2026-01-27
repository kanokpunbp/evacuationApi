using evacuation.Application.DTOs.EvacuationPlan;
using System.Threading.Tasks;

namespace evacuation.Application.Interfaces
{
    public interface IEvacuationPlanService
    {
        Task<bool> CreatePlans();//POST /api/evacuations/plan: Generates a plan
        Task<IReadOnlyList<EvacuationPlanResponseDto>> GetActivePlansAsync();

        Task<bool> UpdatStatus(Guid planId, UpdateEvacuationPlanDto dto); //PUT /api/evacuations/update:  กดบันทึกข้อมูลในแต่ละรอบที่อพยพ 

        Task ClearPlans();// DELETE /api/evacuations/clear:  current evacuation plans

        Task<IReadOnlyList<EvacuationStatusDto>> GetStatusPlan();//GET /api/evacuations/status:
    }
}
