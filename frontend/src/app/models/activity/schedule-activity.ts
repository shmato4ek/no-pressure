import { ScheduleHour } from "../enums/ScheduleHour";
import { ScheduleTime } from "../schedule/schedule-time";

export interface ScheduleActivity {
    name: string;
    id: number; 
    description: string;
    schedule: ScheduleTime[];
}