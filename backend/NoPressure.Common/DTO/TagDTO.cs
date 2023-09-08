using NoPressure.Common.Enums;

namespace NoPressure.Common.DTO
{
    public class TagDTO
    {   
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public List<ActivityDTO> Activities { get; set; }
    }
}
