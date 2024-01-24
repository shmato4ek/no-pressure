using Microsoft.EntityFrameworkCore;
using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;

using EFCore.BulkExtensions;

namespace NoPressure.DAL.Repositories.Impl
{
    public class ActivityRepository : Repository<Activity, int>, IActivityRepository
    {
        public ActivityRepository(NoPressureDbContext context) : base(context) { }

        public async Task BulkInsert(List<Activity> activities)
        {
            await _context.BulkInsertAsync(activities);
        }

        public async Task<List<Activity>> FindAllUserActivitiesAsync(int userId)
        {
            var activities = await _context
                .Activities
                .Include(a => a.Tag)
                .Where(activity => activity.UserId == userId)
                .ToListAsync();

            return activities;
        }

        public async Task<List<Activity>> GetActivitiesByDate(DateTime date)
        {
            var activities = await _context
                .Activities
                .Where(activity => activity.Date.Date == date.Date)
                .ToListAsync();

            return activities;
        }

        public async Task<List<Activity>> GetAllTeamActivities(int teamId)
        {
            var activities = await _context
                .Activities
                .Include(a => a.Tag)
                .Where(a => a.Tag.Team != null && a.Tag.Team.Id == teamId)
                .ToListAsync();

            return activities;            
        }
    }
}
