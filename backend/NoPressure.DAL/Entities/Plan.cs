namespace NoPressure.DAL.Entities
{
    public class Plan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<Activity> Activities { get; set; }
        public bool IsGoal { get; set; } = false;
    }
}
