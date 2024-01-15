import { ScheduleStatistic } from "./schedule-statistic";
import { TagStatistic } from "./tag-statistic";

export interface Statistic {
    followers: number;
    followings: number;
    qualityMonth: number;
    qualityWeek: number;
    qualityAllTime: number;
    tagStatistics: TagStatistic[];
    schedule: ScheduleStatistic[];
}