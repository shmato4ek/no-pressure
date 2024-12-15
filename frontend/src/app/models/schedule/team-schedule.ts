import { ScheduleTime } from "./schedule-time";

export interface TeamSchedule {
    hours: ScheduleTime[];
    date: string;
}