namespace NoPressure.DAL.Entities
{
    public class ScheduleHour
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int Hour { get; set; }
        public Activity Activity { get; set; }
    }
}
