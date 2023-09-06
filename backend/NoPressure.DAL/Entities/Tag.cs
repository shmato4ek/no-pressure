using NoPressure.Common.Enums;

namespace NoPressure.DAL.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public ActivityColor Color { get; set; }
        public List<Activity> Activities { get; set; }
    }
}
