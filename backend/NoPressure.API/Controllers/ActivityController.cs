using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.Models.Activity;

namespace NoPressure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateActivity(NewActivity newActivity)
        {
            await _activityService.CreateActivity(newActivity);
            return NoContent();
        }

        [HttpGet("user/{userId:int}")]
        public async Task<ActionResult> GetAllUserActivities(int userId)
        {
            return Ok(await _activityService.GetAllUserActivity(userId));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateActivity(UpdateActivity updatedActivity)
        {
            return Ok(await _activityService.UpdateActivity(updatedActivity));
        }
    }
}
