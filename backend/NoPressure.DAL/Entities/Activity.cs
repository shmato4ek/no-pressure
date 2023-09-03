using NoPressure.Common.Enums;

namespace NoPressure.DAL.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PlanId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ScheduleHour StartTime { get; set; }
        public ScheduleHour EndTime { get; set; }
        public DateTime Date { get; set; }
    }
}