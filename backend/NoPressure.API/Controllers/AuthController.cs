using Microsoft.AspNetCore.Mvc;
using NoPressure.BLL.JWT;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.Models.User;
using NoPressure.API.Extentions;

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

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUser user)
        {
            return Ok(await _authService.Authorize(user));
        }

        [HttpGet("me")]
        public async Task<ActionResult> GetUserByToken()
        {
            var request = Request.Headers["auth-token"].ToString();
            var token = request[10..(request.Length-2)];
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _userService.GetUserById(userId));
        }

        [HttpGet("check/email/{email}")]
        public async Task<ActionResult> EmailAvailabilityCheck(string email)
        {
            return Ok(await _authService.EmailAvailablityCheck(email));
        }
    }
}
