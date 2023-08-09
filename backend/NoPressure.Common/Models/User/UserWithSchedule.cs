using NoPressure.Common.DTO;

namespace NoPressure.Common.Models.User
{
    public class UserWithSchedule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ActivityDTO> Activities { get; set; }
        public ScheduleDTO Schedule { get; set; }
    }
}
