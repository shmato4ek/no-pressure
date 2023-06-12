﻿using Microsoft.EntityFrameworkCore;
using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.DAL.Repositories.Impl
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(NoPressureDbContext context) : base(context) { }

        public async Task<User> FindUserByEmail(string email)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
            
            if (foundUser is null)
            {
                throw new Exception();
            }

            return foundUser;
        }

        public async Task<User> GetAllInfoById(int id)
        {
            var foundUser = await _context
                .Users
                .Include(user => user.Activities)
                .FirstOrDefaultAsync(user => user.Id == id);

            if (foundUser is null)
            {
                throw new Exception();
            }

            return foundUser;
        }
    }
}