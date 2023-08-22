using Microsoft.AspNetCore.Mvc;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.Models.Plan;

namespace NoPressure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpPost]
        public async Task<ActionResult> AddNewPlan(NewPlan newPlan)
        {
            await _planService.AddNewPlan(newPlan);
            return NoContent();
        }

        [HttpGet("NoGoal")]
        public async Task<ActionResult> GetAllNoGoalPlans(int userId)
        {
            return Ok(await _planService.GetAllNoGoalPlans(userId));
        }
    }
}
