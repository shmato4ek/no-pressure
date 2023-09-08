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

        public ActivityController(IActivityService activityService, ITagService tagService)
        {
            _activityService = activityService;
            _tagService = tagService;
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

        [HttpPut("tag")]
        public async Task<ActionResult> UpdateTag(UpdateTagDTO updateTag)
        {
            await _tagService.UpdateTag(updateTag);
            return NoContent();
        }
    }
}
