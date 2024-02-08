using Microsoft.AspNetCore.Mvc;
using NoPressure.BLL.JWT;
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
        private readonly JwtFactory _jwtFactory;
        public ScheduleController(IScheduleService scheduleService, IActivityService activityService, JwtFactory jwtFactory)
        {
            _scheduleService = scheduleService;
            _activityService = activityService;
            _jwtFactory = jwtFactory;
        }

        [HttpGet("{userId:int}")]
        public async Task<ActionResult> GetScheduleWithActivities(int userId)
        {
            return Ok(await _scheduleService.GetScheduleAndActivities(userId));
        }

        [HttpGet("team/schedule/{teamId}")]
        public async Task<ActionResult> GetTeamScheduleWithActivities(int teamId)
        {
            var request = Request.Headers["auth-token"].ToString();
            var token = request[10..(request.Length-2)];
            var userId = _jwtFactory.GetValueFromToken(token);

            return Ok(await _scheduleService.GetTeamSchedule(teamId, userId));
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

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult> GetTeamSchedule(int teamId)
        {
            return Ok(await _scheduleService.GetTeamSchedule(teamId));
        }
    }
}
