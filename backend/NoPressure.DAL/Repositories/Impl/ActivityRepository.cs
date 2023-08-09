using Microsoft.EntityFrameworkCore;
using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;

namespace NoPressure.DAL.Repositories.Impl
{
    public class ActivityRepository : Repository<Activity, int>, IActivityRepository
    {
        public ActivityRepository(NoPressureDbContext context) : base(context) { }

        public async Task<IEnumerable<Activity>> FindAllUserActivitiesAsync(int userId)
        {
            var activities = await _context
                .Activities
                .Where(activity => activity.UserId == userId)
                .ToListAsync();

            return activities;
        }
    }
}
