using NoPressure.Common.DTO;

namespace NoPressure.Common.Models.Schedule
{
    public class TeamSchedule
    {
        public string Date { get; set; }
        public List<ScheduleTime> Hours { get; set; }
    }
}