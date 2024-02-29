using NoPressure.Common.Auth;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.BLL.Sevices.Abstract
{
    public interface IAuthService
    {
        Task<AuthUser> Authorize(LoginUser loginUser);
        Task<AccessToken> GenerateAccessToken(int id, string userName, string email);
        Task<EmailCheckResults> EmailAvailablityCheck(string email);
        Task<bool> CheckPassword(string password, int userId);
    }
}
