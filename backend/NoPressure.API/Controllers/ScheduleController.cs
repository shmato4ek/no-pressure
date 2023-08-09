using Microsoft.AspNetCore.Mvc;
using NoPressure.BLL.Sevices.Abstract;

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

        [HttpGet]
        public async Task<ActionResult> GetSchedule(DateTime date)
        {
            return Ok(await _scheduleService.GetSchedule(date));
        }

    }
}
