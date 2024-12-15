using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoPressure.Common.Enums;

namespace NoPressure.Common.Models.User
{
    public class ExternalAuthUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
