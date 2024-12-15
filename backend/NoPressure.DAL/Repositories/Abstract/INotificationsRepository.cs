using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface INotificationRepository : IRepository<Notification, int>
    {
        Task<List<Notification>> GetAllUserNotifications(int userId);
        Task<bool> CheckNotifications(int userId);
        Task BulkInsert(List<Notification> notifications);
    }
}
