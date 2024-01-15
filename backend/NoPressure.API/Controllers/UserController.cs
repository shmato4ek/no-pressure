using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoPressure.BLL.JWT;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.User;

namespace NoPressure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtFactory _jwtFactory;
        public UserController(IUserService userService, JwtFactory jwtFactory)
        {
            _userService = userService;
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
            var request = Request.Headers["auth-token"].ToString();
            var token = request[10..(request.Length-2)];
            var userId = _jwtFactory.GetValueFromToken(token);            
            await _userService.Subscribe(userId, subscribe.FollowingId);
            return Ok();
        }

        [HttpDelete("subscriptions/{followingId}")]
        public async Task<ActionResult> UnSubscribe(int followingId)
        {
            var request = Request.Headers["auth-token"].ToString();
            var token = request[10..(request.Length-2)];
            var userId = _jwtFactory.GetValueFromToken(token);
            await _userService.UnSubscribe(userId, followingId);
            return NoContent();            
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var request = Request.Headers["auth-token"].ToString();
            var token = request[10..(request.Length-2)];
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _userService.GetUserByEmail(email, userId));
        }
    }
}
