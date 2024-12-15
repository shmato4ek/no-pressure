using NoPressure.Common.Auth;
using NoPressure.Common.DTO;

namespace NoPressure.Common.Models.User
{
    public class AuthUser
    {
        public UserDTO User { get; set; }
        public AccessToken Token { get; set; }
    }
}
