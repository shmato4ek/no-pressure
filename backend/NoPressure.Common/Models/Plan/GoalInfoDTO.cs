using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Tag;

namespace NoPressure.Common.Models.Plan
{
    public class GoalInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ActivityDTO> ActiveActivities { get; set; }
        public List<ActivityDTO> DoneActivities { get; set; }
        public int Progress { get; set; }
        public int DoneTasksAmmount { get; set; }
        public int AllTasksAmmount { get; set; }
    }
}