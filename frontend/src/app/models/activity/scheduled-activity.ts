import { ActivityState } from "../enums/ActivityState";
import { ScheduleHour } from "../enums/ScheduleHour";
import { ScheduleTime } from "../schedule/schedule-time";

export interface ScheduledActivity {
    name: string;
    id: number; 
    description: string;
    schedule: ScheduleTime[];
    startTime?: ScheduleHour;
    endTime?: ScheduleHour;
    state: ActivityState;
}