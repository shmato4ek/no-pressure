using Microsoft.EntityFrameworkCore;
using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;

namespace NoPressure.DAL.Repositories.Impl
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(NoPressureDbContext context) : base(context) { }

        public async Task<User> FindUserByEmail(string email)
        {
            var foundUser = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Email == email);

            return foundUser;
        }

        public async Task<User> GetAllInfoById(int id)
        {
            var foundUser = await _context
                .Users
                .Include(user => user.Activities)
                .FirstOrDefaultAsync(user => user.Id == id);

            return foundUser;
        }
    }
}
