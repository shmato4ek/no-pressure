namespace NoPressure.DAL.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<ScheduleHour> Time { get; set; }
    }
}
