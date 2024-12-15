using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoPressure.BLL.Exceptions;
using NoPressure.BLL.JWT;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.User;

namespace NoPressure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly JwtFactory _jwtFactory;

        public UserController(IUserService userService, INotificationService notificationService, JwtFactory jwtFactory)
        {
            _userService = userService;
            _notificationService = notificationService;
            _jwtFactory = jwtFactory;
        }

        [HttpGet("subscriptions/{userId}")]
        public async Task<ActionResult> GetUserSubscriptions(int userId)
        {
            return Ok(await _userService.GetUserSubscriptions(userId));
        }

        [HttpPost("subscriptions")]
        public async Task<ActionResult> Subscribe(SubscribeUsers subscribe)
        {
            try
            {
                var request = Request.Headers["auth-token"].ToString();
                var token = request[10..(request.Length-2)];
                var userId = _jwtFactory.GetValueFromToken(token);            
                await _userService.Subscribe(userId, subscribe.FollowingId);
                return Ok();
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

        [HttpDelete("subscriptions/{followingId}")]
        public async Task<ActionResult> UnSubscribe(int followingId)
        {
            try
            {
                var request = Request.Headers["auth-token"].ToString();
                var token = request[10..(request.Length-2)];
                var userId = _jwtFactory.GetValueFromToken(token);
                await _userService.UnSubscribe(userId, followingId);
                return NoContent();    
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

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var request = Request.Headers["auth-token"].ToString();
                var token = request[10..(request.Length-2)];
                var userId = _jwtFactory.GetValueFromToken(token);
                return Ok(await _userService.GetUserByEmail(email, userId));
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

        [HttpGet("settings")]
        public async Task<ActionResult> GetUserSettings()
        {
            try
            {
                var request = Request.Headers["auth-token"].ToString();
                var token = request[10..(request.Length-2)];
                var userId = _jwtFactory.GetValueFromToken(token);
                return Ok(await _userService.GetUserSettings(userId));
            }

            catch (NotAuthorizedException ex)
            {
                return StatusCode(401, ex.Message);
            }
        }

        [HttpPut("settings")]
        public async Task<ActionResult> UpdateSettings(SettingsDTO settings)
        {
            try
            {
                var request = Request.Headers["auth-token"].ToString();
                var token = request[10..(request.Length-2)];
                var userId = _jwtFactory.GetValueFromToken(token);
                await _userService.UpdateUserSettings(settings, userId);
                return Ok();
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

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUser user)
        {
            await _userService.UpdateUser(user);
            return Ok();
        }

        [HttpPut("password")]
        public async Task<ActionResult> ChangePassword(ChangePassword changePassword)
        {
            try
            {
                await _userService.ChangePassword(changePassword);
                return Ok();
            }

            catch (InvalidUserNameOrPasswordException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet("notifications")]
        public async Task<ActionResult> GetNotifications()
        {
            try
            {
                var request = Request.Headers["auth-token"].ToString();
                var token = request[10..(request.Length-2)];
                var userId = _jwtFactory.GetValueFromToken(token);
                return Ok(await _notificationService.GetUserNotifications(userId));
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

        [HttpPut("notifications/{id}")]
        public async Task<ActionResult> CheckNotification(int id)
        {
            try
            {
                await _notificationService.CheckNotification(id);
                return Ok();
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
