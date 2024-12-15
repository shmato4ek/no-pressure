namespace NoPressure.Common.DTO
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<ScheduleHourDTO> Time { get; set; }
    }
}
