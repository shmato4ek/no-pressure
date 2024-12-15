using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> FindUserByEmail(string email);
        Task<User> GetAllInfoById(int id);
    }
}
