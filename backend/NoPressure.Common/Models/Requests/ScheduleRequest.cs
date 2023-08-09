using NoPressure.Common.DTO;

namespace NoPressure.Common.Models.Requests
{
    public class ScheduleRequest
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}