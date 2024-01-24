using NoPressure.Common.DTO;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.Schedule;

namespace NoPressure.BLL.Sevices.Abstract
{
    public interface IScheduleService
    {
        Task<Schedule> GetScheduleAndActivities(int userId);
        Task<TeamSchedule> GetTeamSchedule(int userId);
        Task AddActivityToSchedule(AddTaskToSchedule activity);
        int GetHoursOfDoneTasks(List<ActivityDTO> activities);
    }
}
