namespace NoPressure.Common.DTO
{
    public class PlanDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<ActivityDTO> Activities { get; set; }
        public bool IsGoal { get; set; } = false;
    }
}