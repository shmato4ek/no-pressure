using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Tag;

namespace NoPressure.Common.Models.Plan
{
    public class AllGoals
    {
        public List<GoalInfoDTO> ActiveGoals { get; set; }
        public List<GoalInfoDTO> ClosedGoals { get; set; }
    }
}