using NoPressure.Common.DTO;
using NoPressure.Common.Models.Notifications;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.Schedule;
using NoPressure.Common.Models.Tag;

namespace NoPressure.BLL.Sevices.Abstract
{
    public interface INotificationService
    {
        Task<List<NotificationDTO>> GetUserNotifications(int userId);
        Task<bool> CheckNotifications(int userId);
        Task CheckNotification(int id);
    }
}
