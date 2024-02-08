using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface IActivityRepository : IRepository<Activity, int>
    {
        Task<List<Activity>> FindAllUserActivitiesAsync(int userId);
        Task<List<Activity>> GetActivitiesByDate(DateTime date);
        Task BulkInsert(List<Activity> activities);
        Task<List<Activity>> GetAllTeamActivities(int teamId);
        Task<List<Activity>> GetAllUserActivitiesWithoutTeam(int userId);
    }
}
