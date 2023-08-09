using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface IActivityRepository : IRepository<Activity, int>
    {
        Task<IEnumerable<Activity>> FindAllUserActivitiesAsync(int userId);
    }
}
