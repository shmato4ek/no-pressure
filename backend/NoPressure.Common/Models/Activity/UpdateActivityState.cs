using NoPressure.Common.Enums;

namespace NoPressure.Common.Models.Activity
{
    public class UpdateActivityState
    {
        public int Id { get; set; }
        public ActivityState State { get; set; }
    }
}