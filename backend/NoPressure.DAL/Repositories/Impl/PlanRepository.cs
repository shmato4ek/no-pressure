using Microsoft.EntityFrameworkCore;
using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;

namespace NoPressure.DAL.Repositories.Impl
{
    public class PlanRepository : Repository<Plan, int>, IPlanRepository
    {
        public PlanRepository(NoPressureDbContext context) : base(context) { }

        public async Task<List<Plan>> GetAllNoGoalPlans(int userId)
        {
            var plans = await _context
                .Plans
                .Where(plan => plan.UserId == userId)
                .Where(plan => plan.IsGoal == false)
                .ToListAsync();

            return plans;
        }
    }
}
