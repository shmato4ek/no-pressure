using NoPressure.Common.Enums;

namespace NoPressure.Common.Models.Requests
{
    public class AddTaskToSchedule
    {
        public int ActivityId { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}