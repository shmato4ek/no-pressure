import { ActivityDTO } from "../activity/activity-dto";
import { ScheduleHour } from "../enums/ScheduleHour";

export interface ScheduleTime {
    hour: ScheduleHour;
    activity?: ActivityDTO;
    hasPrevious: boolean;
    hasNext: boolean;
}