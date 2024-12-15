using Microsoft.AspNetCore.Mvc;
using NoPressure.BLL.JWT;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.Models.User;
using NoPressure.API.Extentions;
using NoPressure.BLL.Exceptions;

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
            try
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

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }

            catch (ExistUserException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUser user)
        {
            try
            {
            return Ok(await _authService.Authorize(user));
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }

            catch (InvalidUserNameOrPasswordException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("me")]
        public async Task<ActionResult> GetUserByToken()
        {
            try
            {
                var request = Request.Headers["auth-token"].ToString();
                var token = request[10..(request.Length-2)];
                var userId = _jwtFactory.GetValueFromToken(token);
                return Ok(await _userService.GetUserById(userId));
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }

            catch (NotAuthorizedException ex)
            {
                return StatusCode(401, ex.Message);
            }

            catch (ArgumentOutOfRangeException)
            {
                return StatusCode(401);
            }        
        }

        [HttpPost("google")]
        public async Task<ActionResult> GoogleAuth(ExternalAuthUser user)
        {
            return Ok(await _userService.GoogleAuth(user));
        }

        [HttpGet("check/email/{email}")]
        public async Task<ActionResult> EmailAvailabilityCheck(string email)
        {
            return Ok(await _authService.EmailAvailablityCheck(email));
        }

        [HttpGet("password/{password}")]
        public async Task<ActionResult> CheckPassword(string password)
        {
            try
            {
                var request = Request.Headers["auth-token"].ToString();
                var token = request[10..(request.Length-2)];
                var userId = _jwtFactory.GetValueFromToken(token);
                return Ok(await _authService.CheckPassword(password, userId));
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }

            catch (NotAuthorizedException ex)
            {
                return StatusCode(401, ex.Message);
            }
        }
    }
}
