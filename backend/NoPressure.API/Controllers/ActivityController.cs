using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(await _activityService.GetAllUserActivity(userId));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateActivity(UpdateActivity updatedActivity)
        {
            return Ok(await _activityService.UpdateActivity(updatedActivity));
        }

        [HttpDelete("{activityId:int}")]
        public async Task<ActionResult> DeleteActivity(int activityId)
        {
            await _activityService.DeleteActivity(activityId);
            return NoContent();
        }

        [HttpGet("tag/{userId}")]
        public async Task<ActionResult> GetTagsInfo(int userId)
        {
            return Ok(await _tagService.GetUsersTagInfo(userId));
        }

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult> GetTeamTagsInfo(int teamId)
        {
            return Ok(await _tagService.GetTeamTagInfo(teamId));
        }

        [HttpPut("tag")]
        public async Task<ActionResult> UpdateTag(UpdateTagDTO updateTag)
        {
            await _tagService.UpdateTag(updateTag);
            return NoContent();
        }

        [HttpPatch("state")]
        public async Task<ActionResult> ChangeState(UpdateActivityState activity)
        {
            await _activityService.ChangeState(activity);
            return NoContent();
        }

        [HttpGet("statistic/{userId}")]
        public async Task<ActionResult> GetStatistic(int userId)
        {
            return Ok(await _statisticService.GetActivitiesStatistic(userId));
        }
    }
}
