using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface IActivityRepository : IRepository<Activity, int>
    {
        Task<List<Activity>> FindAllUserActivitiesAsync(int userId);
        Task<List<Activity>> GetActivitiesByDate(DateTime date);
    }
}
