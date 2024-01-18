using NoPressure.Common.Enums;

namespace NoPressure.Common.DTO
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string? Link { get; set; }
        public bool IsRead { get; set; }
    }
}
