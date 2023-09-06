import { ActivityDTO } from "../activity/activity-dto";
import { ActivityColor } from "../enums/activity-color";
import { ScheduleTime } from "./schedule-time";

export interface Tag {
    id: number;
    userId: number;
    name: string;
    color: ActivityColor;
    activities: ActivityDTO[];
}
