using NoPressure.Common.Enums;

namespace NoPressure.Common.Models.Activity
{
    public class UpdateActivity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ScheduleHour StartTime { get; set; }
        public ScheduleHour EndTime { get; set; }
        public ScheduleHour DirectiveTerm { get; set; }
        public int Priority { get; set; }
        public double DelayCoefficient { get; set; }
        public int Duration { get; set; }
    }
}