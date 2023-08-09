using NoPressure.Common.DTO;

namespace NoPressure.BLL.Sevices.Abstract
{
    public interface IScheduleService
    {
        Task<ScheduleDTO> GetSchedule(DateTime date);
        Task<ScheduleDTO> AddActivityToSchedule(DateTime date, int activityId, int hour);
    }
}
