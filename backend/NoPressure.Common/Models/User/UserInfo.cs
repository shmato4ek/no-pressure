using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Team;

namespace NoPressure.Common.Models.User
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RegistrationDate { get; set; }
        public bool IsNotificationsChecked { get; set; }
        public List<ActivityDTO> Activities { get; set; }
        public List<TeamInfo>? Teams { get; set; }
        public AuthType AuthType { get; set; }
    }
}
