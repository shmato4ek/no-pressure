using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Plan;

namespace NoPressure.BLL.Sevices.Abstract
{
    public interface IPlanService
    {
        Task AddNewPlan(NewPlan newPlan);
        Task<List<PlanDTO>> GetAllNoGoalPlans(int userId);
    }
}
