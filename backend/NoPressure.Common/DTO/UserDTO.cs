using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoPressure.Common.Enums;

namespace NoPressure.Common.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public List<ActivityDTO> Activities { get; set; }
        public AuthType AuthType { get; set; }
    }
}
