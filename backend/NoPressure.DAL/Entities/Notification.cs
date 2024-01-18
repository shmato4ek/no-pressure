using NoPressure.Common.Enums;
using NoPressure.Common.Models.Plan;

namespace NoPressure.DAL.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public NotificationTitle Title { get; set; }
        public NotificationData Data { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; } = false;
    }
}