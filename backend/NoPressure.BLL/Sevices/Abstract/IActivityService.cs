using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Tag;

namespace NoPressure.BLL.Sevices.Abstract
{
    public interface IActivityService
    {
        Task CreateActivity(NewActivity newActivity);
        Task<List<ActivityDTO>> GetAllUserActivity(int userId);
        Task<ActivityDTO> UpdateActivity(UpdateActivity updatedActivity);
        Task DeleteActivity(int activityId);
        Task<ActivityDTO> GetActivityById(int activityId);
        Task RemoveFromSchedule(int activityId);
        Task ChangeState(UpdateActivityState updateActivity);
        Task<List<ActivityDTO>> GetActivitiesByDate(DateTime date);
        Task AddNewGoalActivities(List<NewActivity> activities, int tagId, int planId);
        Task<int> CreateTag(NewTag newTag);
    }
}
