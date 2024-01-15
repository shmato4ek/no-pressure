using NoPressure.Common.DTO;
using NoPressure.Common.Statistic;

namespace NoPressure.Common.Models.Statistic
{
    public class ActivitiesStatistic
    {
        public int Followers { get; set; }
        public int Followings { get; set; }
        public double QualityMonth { get; set; }
        public double QualityWeek { get; set; }
        public double QualityAllTime { get; set; }
        public List<TagStatistic> TagStatistics { get; set; }
        public List<ScheduleStatistic> Schedule { get; set; }
    }
}