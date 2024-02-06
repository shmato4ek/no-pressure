using System.Drawing;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;

namespace NoPressure.Common.Models.Team
{
    public class UpdateTeamSettings
    {
        public int TeamId { get; set; }
        public TeamPrivacyState State { get; set; }
        public string Color { get; set; }
        public List<UpdateSettings> Settings { get; set; }
    }
}