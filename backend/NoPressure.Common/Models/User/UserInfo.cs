using NoPressure.Common.DTO;

namespace NoPressure.Common.Models.User
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsNotificationsChecked { get; set; }
        public List<ActivityDTO> Activities { get; set; }
    }
}
