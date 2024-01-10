using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("subscriptions/{userId}")]
        public async Task<ActionResult> GetUserSubscriptions(int userId)
        {
            return Ok(await _userService.GetUserSubscriptions(userId));
        }

        [HttpPost("subscriprions")]
        public async Task<ActionResult> Subscribe(SubscribeUsers subscribe)
        {
            await _userService.Subscribe(subscribe.FollowerId, subscribe.FollowingId);
            return Ok();
        }
    }
}
