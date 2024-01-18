using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;

namespace NoPressure.DAL.Repositories.Impl
{
    public class NotificationRepository : Repository<Notification, int>, INotificationRepository
    {
        public NotificationRepository(NoPressureDbContext context) : base(context) { }

        public async Task<bool> CheckNotifications(int userId)
        {
            var notifications = await GetAllUserNotifications(userId);

            var isChecked = true;

            if(notifications.Count != 0)
            {
                foreach(var notification in notifications)
                {
                    if(!notification.IsRead)
                    {
                        isChecked = false;
                    }
                }
            }

            return isChecked;
        }

        public async Task<List<Notification>> GetAllUserNotifications(int userId)
        {
            var notifications = await _context
                .Notifications
                .Include(n => n.Data)
                .Where(n => n.UserId == userId)
                .ToListAsync();

            return notifications;
        }
    }
}
