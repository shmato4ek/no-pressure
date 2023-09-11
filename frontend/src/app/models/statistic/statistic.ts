import { ScheduleStatistic } from "./schedule-statistic";
import { TagStatistic } from "./tag-statistic";

export interface Statistic {
    qualityMonth: number;
    qualityWeek: number;
    qualityAllTime: number;
    tagStatistics: TagStatistic[];
    schedule: ScheduleStatistic[];
}