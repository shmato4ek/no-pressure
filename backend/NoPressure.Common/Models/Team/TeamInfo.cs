using System.Drawing;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Tag;

namespace NoPressure.Common.Models.Team
{
    public class TeamInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Color { get; set; }
        public TeamAccess AddingActivities { get; set; } = TeamAccess.Deny;
        public List<TeamTag>? Tags { get; set; }
    }
}