using NoPressure.Common.DTO;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.Schedule;
using NoPressure.DAL.Entities;

namespace NoPressure.BLL.Sevices.Abstract
{
    public interface IScheduleService
    {
        Task<Schedule> GetScheduleAndActivities(int userId);
        Task<TeamSchedule> GetTeamSchedule(int userId);
        Task AddActivityToSchedule(AddTaskToSchedule activity);
        int GetHoursOfDoneTasks(List<ActivityDTO> activities);
        Task<Schedule> GetTeamSchedule(int teamId, int userId);
        Task GenerateSchedule(int userId, ScheduleGenerationConfigurationDTO config);
        Task<ScheduleGenerationConfigurationDTO> GetScheduleGenerationConfiguration(int userId);
        Task UpdateScheduleGenerationConfiguration(ScheduleGenerationConfigurationDTO config);
    }
}
