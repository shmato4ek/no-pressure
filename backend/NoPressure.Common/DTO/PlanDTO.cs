using NoPressure.Common.Enums;

namespace NoPressure.Common.DTO
{
    public class PlanDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<ActivityDTO> Activities { get; set; }
        public PlanState State { get; set; }
    }
}