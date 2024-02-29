using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface IPlanRepository : IRepository<Plan, int>
    {
        Task<List<Plan>> GetAllNoGoalPlans(int userId);
        Task<List<Plan>> GetAllActiveGoals(int userId);
        Task<List<Plan>> GetAllClosedGoals(int userId);
        Task<Plan> GetPlanByUserIdAsync(int userId);
        Task<Plan> GetGoalById(int goalId);
    }
}
