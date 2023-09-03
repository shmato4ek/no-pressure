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
        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
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
    }
}
