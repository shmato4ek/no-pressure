using NoPressure.Common.Auth;
using NoPressure.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.Common.Models.User
{
    public class AuthUser
    {
        public UserDTO User { get; set; }
        public AccessToken Token { get; set; }
    }
}
