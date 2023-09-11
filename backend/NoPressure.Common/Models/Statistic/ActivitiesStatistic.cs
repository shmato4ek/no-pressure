using NoPressure.Common.DTO;
using NoPressure.Common.Statistic;

namespace NoPressure.Common.Models.Statistic
{
    public class ActivitiesStatistic
    {
        public double QualityMonth { get; set; }
        public double QualityWeek { get; set; }
        public double QualityAllTime { get; set; }
        public List<TagStatistic> TagStatistics { get; set; }
        public List<ScheduleStatistic> Schedule { get; set; }
    }
}