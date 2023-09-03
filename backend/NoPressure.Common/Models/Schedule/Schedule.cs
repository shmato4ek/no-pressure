using NoPressure.Common.DTO;

namespace NoPressure.Common.Models.Schedule
{
    public class Schedule
    {
        public List<ActivityDTO> Activities { get; set; }
        public List<ScheduleTime> Hours { get; set; }
    }
}