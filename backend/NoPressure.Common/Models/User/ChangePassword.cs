using NoPressure.Common.DTO;

namespace NoPressure.Common.Models.User
{
    public class ChangePassword
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
