using NoPressure.Common.Enums;
using NoPressure.Common.Models.Tag;
using NoPressure.Common.Models.User;

namespace NoPressure.Common.DTO
{
    public class TeamWithSettingsDTO
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public TeamPrivacyState State { get; set; }
        public List<TeamSettingsDTO> Settings { get; set; }
    }
}