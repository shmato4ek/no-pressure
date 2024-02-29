using AutoMapper;
using Microsoft.Extensions.Configuration;
using NoPressure.BLL.Exceptions;
using NoPressure.BLL.JWT;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.Auth;
using NoPressure.Common.DTO;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.User;
using NoPressure.Common.Security;
using NoPressure.DAL.Repositories.Abstract;

namespace NoPressure.BLL.Sevices.Impl
{
    public class AuthService : IAuthService
    {
        private readonly JwtFactory _jwtFactory;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthService(JwtFactory jwtFactory, IMapper mapper, IConfiguration configuration, IUserRepository userRepository) 
        {
            _jwtFactory = jwtFactory;
            _mapper = mapper;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<AuthUser> Authorize(LoginUser loginUser)
        {
            var userEntity = await _userRepository.FindUserByEmail(loginUser.Email);

            if (userEntity is null)
            {
                throw new NotFoundException("User");
            }

            if (!SecurityHelper.IsValidPassword(userEntity.Password, loginUser.Password, userEntity.Salt))
            {
                throw new InvalidUserNameOrPasswordException();
            }

            var token = await GenerateAccessToken(userEntity.Id, userEntity.Name, userEntity.Email);
            var user = _mapper.Map<UserDTO>(userEntity);

            return new AuthUser
            {
                Token = token,
                User = user,
            };
        }

        public async Task<AccessToken> GenerateAccessToken(int id, string userName, string email)
        {
            string accessToken = await _jwtFactory.GenerateAccessToken(id, userName, email);
            return new AccessToken(accessToken);
        }

        public async Task<EmailCheckResults> EmailAvailablityCheck(string email)
        {
            var userEntity = await _userRepository.FindUserByEmail(email);
            
            return new EmailCheckResults
            {
              Email = email,
              Availability = userEntity is null,  
            };
        }

        public async Task<bool> CheckPassword(string password, int userId)
        {
            var userEntity = await _userRepository.FindAsync(userId);

            if (userEntity is null)
            {
                throw new NotFoundException("User", userId);
            }

            var data = Convert.FromBase64String(password);

            var decodedPassword = System.Text.Encoding.UTF8.GetString(data);

            var isValid = SecurityHelper.IsValidPassword(userEntity.Password, decodedPassword, userEntity.Salt);

            return isValid;
        }
    }
}
