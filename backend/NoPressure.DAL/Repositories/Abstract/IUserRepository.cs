using NoPressure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> FindUserByEmail(string email);
    }
}
