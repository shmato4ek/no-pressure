﻿using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;

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
    }
}
