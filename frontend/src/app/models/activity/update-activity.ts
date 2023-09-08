import { ScheduleHour } from "../enums/ScheduleHour";

export interface UpdateActivity {
    id: number;
    name: string;
    description: string;
    startTime?: ScheduleHour;
    endTime?: ScheduleHour;
}