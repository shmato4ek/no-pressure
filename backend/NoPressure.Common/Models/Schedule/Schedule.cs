using NoPressure.Common.DTO;

namespace NoPressure.Common.Models.Schedule
{
    public class Schedule
    {
        public string Date { get; set; }
        public List<TagDTO> Tags { get; set; }
        public List<ScheduleTime> Hours { get; set; }
    }
}