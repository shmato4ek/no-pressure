using System.ComponentModel.DataAnnotations.Schema;
using NoPressure.Common.Enums;

namespace NoPressure.DAL.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ScheduleHour StartTime { get; set; }
        public ScheduleHour EndTime { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreationDate { get; set; }

        public bool IsRepeatable { get; set; } = false;
        public bool IsScheduled { get; set; } = false;
        public ActivityState State { get; set; } = ActivityState.Active;

        public int UserId { get; set; }
        public int? PlanId { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}