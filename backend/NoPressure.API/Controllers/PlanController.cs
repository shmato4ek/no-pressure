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

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetAllNoGoalPlans(int userId)
        {
            return Ok(await _planService.GetAllNoGoalPlans(userId));
        }

        [HttpGet("goals/{userId}")]
        public async Task<ActionResult> GetAllGoals(int userId)
        {
            return Ok(await _planService.GetAllGoals(userId));
        }

        [HttpPatch("state")]
        public async Task<ActionResult> ChangeState(PlanChangeState updatedPlan)
        {
            await _planService.ChangeState(updatedPlan);
            return NoContent();
        }

        [HttpPatch("goal")]
        public async Task<ActionResult> ConvertToGoal(GoalDTO goal)
        {
            await _planService.ConvertToGoal(goal);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePlan(UpdatePlan updatedPlan)
        {
            await _planService.UpdatePlan(updatedPlan);
            return NoContent();
        }

        [HttpDelete("{planId}")]
        public async Task<ActionResult> DeletePlan(int planId)
        {
            await _planService.DeletePlan(planId);
            return NoContent();
        }
    }
}
