using Microsoft.AspNetCore.Mvc;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.Schedule;

namespace NoPressure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IActivityService _activityService;
        public ScheduleController(IScheduleService scheduleService, IActivityService activityService)
        {
            _scheduleService = scheduleService;
            _activityService = activityService;
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult> GetScheduleWithActivities(int userId)
        {
            return Ok(await _scheduleService.GetScheduleAndActivities(userId));
        }

        [HttpPost]
        public async Task<ActionResult> AddTaskToSchedule(AddTaskToSchedule activity)
        {
            await _scheduleService.AddActivityToSchedule(activity);
            return NoContent();
        }

        [HttpDelete("{activityId}")]
        public async Task<ActionResult> RemoveFromSchedule(int activityId)
        {
            await _activityService.RemoveFromSchedule(activityId);
            return NoContent();
        }
    }
}
