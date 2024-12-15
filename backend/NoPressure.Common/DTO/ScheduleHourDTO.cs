namespace NoPressure.Common.DTO
{
    public class ScheduleHourDTO
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int Hour { get; set; }
        public ActivityDTO Activity { get; set; }
    }
}
