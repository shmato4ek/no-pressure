using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoPressure.BLL.Exceptions;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Tag;
using NoPressure.DAL.Entities;

namespace NoPressure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly ITagService _tagService;
        private readonly IStatisticService _statisticService;

        public ActivityController(IActivityService activityService,
                                    ITagService tagService,
                                    IStatisticService statisticService)
        {
            _activityService = activityService;
            _tagService = tagService;
            _statisticService = statisticService;
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
            try
            {
                return Ok(await _activityService.GetAllUserActivity(userId));
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateActivity(UpdateActivity updatedActivity)
        {
            try
            {
                return Ok(await _activityService.UpdateActivity(updatedActivity));
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpDelete("{activityId:int}")]
        public async Task<ActionResult> DeleteActivity(int activityId)
        {
            try
            {
                await _activityService.DeleteActivity(activityId);
                return NoContent();
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet("tag/{userId}")]
        public async Task<ActionResult> GetTagsInfo(int userId)
        {
            try
            {
                return Ok(await _tagService.GetUsersTagInfo(userId));
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult> GetTeamTagsInfo(int teamId)
        {
            try
            {
                return Ok(await _tagService.GetTeamTagInfo(teamId));
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPut("tag")]
        public async Task<ActionResult> UpdateTag(UpdateTagDTO updateTag)
        {
            try
            {
                await _tagService.UpdateTag(updateTag);
                return NoContent();
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPatch("state")]
        public async Task<ActionResult> ChangeState(UpdateActivityState activity)
        {
            try
            {
                await _activityService.ChangeState(activity);
                return NoContent();
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet("statistic/{userId}")]
        public async Task<ActionResult> GetStatistic(int userId)
        {
            try
            {
                return Ok(await _statisticService.GetActivitiesStatistic(userId));
            }

            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }
    }
}
