using NoPressure.Common.Enums;

namespace NoPressure.DAL.Entities
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Activity> Activities { get; set; }
        public PlanState State { get; set; } = PlanState.Plan;
        public GoalState GoalState { get; set; } = GoalState.Active;

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
