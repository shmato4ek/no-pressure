using System.Drawing;
using NoPressure.Common.DTO;

namespace NoPressure.Common.Models.Tag
{
    public class TeamTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public List<ActivityDTO> Activities { get; set; }
        public int ActivitiesDone { get; set; }
        public int ActivitiesAll { get; set; }
    }
}