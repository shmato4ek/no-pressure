using NoPressure.Common.DTO;
using NoPressure.Common.Enums;

namespace NoPressure.Common.Models.Schedule
{
    public class ScheduleTime
    {
        public ScheduleHour Hour { get; set; }
        public ActivityDTO Activity { get; set; } 
        public bool HasPrevious { get; set; } = false;
        public bool HasNext { get; set; } = false;
    }
}