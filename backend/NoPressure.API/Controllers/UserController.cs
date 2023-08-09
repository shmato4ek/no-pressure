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

        [HttpPost]
        public async Task<ActionResult> GetUserWithSchedule(ScheduleRequest scheduleInfo)
        {
            return Ok(await _userService.GetUserWithSchedule(scheduleInfo));
        }
    }
}
