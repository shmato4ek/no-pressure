import { ActivityState } from "../enums/ActivityState";
import { ScheduleHour } from "../enums/ScheduleHour";

export interface ActivityDTO {
    id: number;
    userId: number;
    name: string;
    description: string;
    color: string;
    state: ActivityState;
    startTime?: ScheduleHour;
    endTime?: ScheduleHour;
}