using Microsoft.EntityFrameworkCore;
using NoPressure.Common.Enums;
using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;

namespace NoPressure.DAL.Repositories.Impl
{
    public class PlanRepository : Repository<Plan, int>, IPlanRepository
    {
        public PlanRepository(NoPressureDbContext context) : base(context) { }

        public async Task<List<Plan>> GetAllGoals(int userId)
        {
            var plans = await _context
                .Plans
                .Include(p => p.Activities)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return plans;
        }

        public async Task<List<Plan>> GetAllNoGoalPlans(int userId)
        {
            var plans = await _context
                .Plans
                .Where(plan => plan.UserId == userId)
                .Where(plan => plan.State == PlanState.Plan)
                .ToListAsync();

            return plans;
        }

        public async Task<Plan> GetPlanByUserIdAsync(int userId)
        {
            var plan = await _context
                .Plans
                .Where(plan => plan.UserId == userId)
                .FirstOrDefaultAsync();

            return plan;
        }
    }
}
