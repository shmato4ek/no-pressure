using NoPressure.Common.Enums;

namespace NoPressure.Common.Models.Activity
{
    public class NewActivity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Tag { get; set; }
        public string Color { get; set; }
        public bool IsRepeatable { get; set; }
    }
}
