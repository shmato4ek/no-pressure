using NoPressure.Common.Enums;

namespace NoPressure.Common.Models.Activity
{
    public class NewActivity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Tag { get; set; }
        public string Color { get; set; }
        public bool IsRepeatable { get; set; }
        public ScheduleHour DirectiveTerm { get; set; }
        public int Priority { get; set; }
        public double DelayCoefficient { get; set; }
        public int Duration { get; set; }
        
        public int? TeamId { get; set; }
        public int UserId { get; set; }
    }
}