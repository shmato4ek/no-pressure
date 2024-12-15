using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.Common.Auth
{
    public class AccessToken
    {
        public string Token { get; set; }
        public AccessToken(string token)
        {
            Token = token;
        }
    }
}
