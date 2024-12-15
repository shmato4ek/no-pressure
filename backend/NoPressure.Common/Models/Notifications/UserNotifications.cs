using NoPressure.Common.DTO;

namespace NoPressure.Common.Models.Notifications
{
    public class UserNotifications
    {
        public List<NotificationDTO> Notifications { get; set; }
        public bool IsChecked { get; set; } = true;
    }
}