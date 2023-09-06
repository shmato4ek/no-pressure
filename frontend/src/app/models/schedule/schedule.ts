import { ScheduleTime } from "./schedule-time";
import { Tag } from "./tag";

export interface Schedule {
    tags: Tag[];
    hours: ScheduleTime[];
    date: string;
}