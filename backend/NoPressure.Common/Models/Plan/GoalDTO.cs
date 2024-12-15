using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Tag;

namespace NoPressure.Common.Models.Plan
{
    public class GoalDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public NewTag Tag { get; set; }
        public List<NewActivity> Activities { get; set; }
    }
}