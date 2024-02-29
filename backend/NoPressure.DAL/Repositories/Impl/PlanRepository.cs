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

        public async Task<List<Plan>> GetAllActiveGoals(int userId)
        {
            var plans = await _context
                .Plans
                .Include(p => p.Activities)
                .Where(p => p.UserId == userId)
                .Where(p => p.State == PlanState.Goal && p.GoalState == GoalState.Active)
                .ToListAsync();

            return plans;
        }

        public async Task<List<Plan>> GetAllClosedGoals(int userId)
        {
            var plans = await _context
                .Plans
                .Include(p => p.Activities)
                .Where(p => p.UserId == userId)
                .Where(p => p.State == PlanState.Goal && p.GoalState == GoalState.Closed)
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

        public async Task<Plan> GetGoalById(int goalId)
        {
            var goal = await _context
                .Plans
                .Where(p => p.State == PlanState.Goal)
                .FirstOrDefaultAsync(p => p.Id == goalId);

            return goal;
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
