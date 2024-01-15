using Microsoft.EntityFrameworkCore;
using NoPressure.Common.Enums;
using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;

namespace NoPressure.DAL.Repositories.Impl
{
    public class SubscriptionRepository : Repository<Subscription, int>, ISubscriptionRepository
    {
        public SubscriptionRepository(NoPressureDbContext context) : base(context) { }

        public async Task<List<Subscription>> GetAllUsersFollowers(int userId)
        {
            var followers = await _context
                .Subscriptions
                .Include(s => s.Follower)
                .Where(s => s.FollowingId == userId)
                .ToListAsync();

            return followers;
        }

        public async Task<List<Subscription>> GetAllUsersFollowings(int userId)
        {
            var followings = await _context
                .Subscriptions
                .Include(s => s.Following)
                .Where(s => s.FollowerId == userId)
                .ToListAsync();

            return followings;
        }

        public async Task UnSubscribe(int followerId, int followingId)
        {
            var subscription = await _context
                .Subscriptions
                .Where(s => s.FollowerId == followerId)
                .Where(s => s.FollowingId == followingId)
                .FirstOrDefaultAsync();

            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
            }
        }
    }
}
