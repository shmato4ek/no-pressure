using Microsoft.AspNetCore.Mvc;
using NoPressure.BLL.Exceptions;
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
            try
            {
                return Ok(await _planService.GetAllNoGoalPlans(userId));
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet("goals/{userId}")]
        public async Task<ActionResult> GetAllGoals(int userId)
        {
            try
            {
                return Ok(await _planService.GetAllGoals(userId));
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPatch("state")]
        public async Task<ActionResult> ChangeState(PlanChangeState updatedPlan)
        {
            try
            {
                await _planService.ChangeState(updatedPlan);
                return NoContent();
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPatch("goal/state")]
        public async Task<ActionResult> ChangeGoalState(GoalChangeState goal)
        {
            try
            {
                await _planService.ChangeGoalState(goal);
                return NoContent();
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }        
        }

        [HttpPatch("goal")]
        public async Task<ActionResult> ConvertToGoal(GoalDTO goal)
        {
            try
            {
                await _planService.ConvertToGoal(goal);
                return NoContent();
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePlan(UpdatePlan updatedPlan)
        {
            try
            {
                await _planService.UpdatePlan(updatedPlan);
                return NoContent();
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpDelete("{planId}")]
        public async Task<ActionResult> DeletePlan(int planId)
        {
            try
            {
                await _planService.DeletePlan(planId);
                return NoContent();
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }
    }
}
