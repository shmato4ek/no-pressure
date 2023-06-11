using Microsoft.AspNetCore.Mvc;
using NoPressure.BLL.JWT;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.Models.User;

namespace NoPressure.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly JwtFactory _jwtFactory;

        public AuthController(IUserService userService, IAuthService authService, JwtFactory jwtFactory)
        {
            _userService = userService;
            _authService = authService;
            _jwtFactory = jwtFactory;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(NewUser newUser)
        {
            var createdUser = await _userService.CreateUser(newUser);
            var token = await _authService.GenerateAccessToken(createdUser.Id, createdUser.Name, createdUser.Email);
            var result = new AuthUser
            {
                Token = token,
                User = createdUser
            };
            return Ok(result);
        }
    }
}
