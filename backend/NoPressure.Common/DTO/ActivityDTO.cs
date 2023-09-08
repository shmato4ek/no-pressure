﻿using NoPressure.Common.Enums;

namespace NoPressure.Common.DTO
{
    public class ActivityDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Color { get; set; }
        
        public ScheduleHour StartTime { get; set; }
        public ScheduleHour EndTime { get; set; }
        public DateTime Date { get; set; }

        public bool IsRepeatable { get; set; }
    }
}
