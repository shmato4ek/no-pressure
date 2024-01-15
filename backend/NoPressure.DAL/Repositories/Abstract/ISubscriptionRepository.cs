using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface ISubscriptionRepository : IRepository<Subscription, int>
    {
        Task<List<Subscription>> GetAllUsersFollowers(int userId);
        Task<List<Subscription>> GetAllUsersFollowings(int userId);
        Task UnSubscribe (int followerId, int followingId);
    }
}
