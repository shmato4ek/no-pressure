using NoPressure.Common.DTO;
using NoPressure.Common.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.BLL.Sevices.Abstract
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(NewUser newUser);
        Task<UserInfo> GetUserById(int id);
    }
}
