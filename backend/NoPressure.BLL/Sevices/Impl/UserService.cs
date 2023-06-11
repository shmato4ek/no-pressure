using AutoMapper;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Models.User;
using NoPressure.Common.Security;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Unit.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.BLL.Sevices.Impl
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork ouw, IMapper mapper)
        {
            _uow = ouw;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUser(NewUser newUser)
        {
            var isUserExist = await _uow.UserRepository.FindUserByEmail(newUser.Email);
            
            if (isUserExist != null)
            {
                throw new Exception();
            }

            var salt = SecurityHelper.GetRandomBytes();

            var userEntity = _mapper.Map<User>(newUser);
            userEntity.Salt = Convert.ToBase64String(salt);
            userEntity.Password = SecurityHelper.HashPassword(newUser.Password, salt);
            
            _uow.UserRepository.Create(userEntity);

            await _uow.SaveAsync();

            return _mapper.Map<UserDTO>(userEntity);
        }
    }
}
