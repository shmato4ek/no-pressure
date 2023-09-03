import { ActivityDTO } from "../activity/activity-dto";
import { ScheduleTime } from "./schedule-time";

export interface Schedule {
    activities: ActivityDTO[];
    hours: ScheduleTime[];
}