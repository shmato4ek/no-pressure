using NoPressure.Common.DTO;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.Schedule;
using NoPressure.Common.Models.Statistic;
using NoPressure.Common.Models.Tag;

namespace NoPressure.BLL.Sevices.Abstract
{
    public interface IStatisticService
    {
        Task<ActivitiesStatistic> GetActivitiesStatistic(int userId);
    }
}
