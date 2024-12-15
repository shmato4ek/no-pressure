using NoPressure.Common.DTO;
using NoPressure.Common.Models.Statistic;

namespace NoPressure.Common.Models.User
{
    public class UserShared
    {
        public UserInfo User { get; set; }
        public ActivitiesStatistic Statistic { get; set; }
        public bool IsFollowed { get; set; }
    }
}
