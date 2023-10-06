using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface IPlanRepository : IRepository<Plan, int>
    {
        Task<List<Plan>> GetAllNoGoalPlans(int userId);
        Task<List<Plan>> GetAllGoals(int userId);
        Task<Plan> GetPlanByUserIdAsync(int userId);
    }
}
