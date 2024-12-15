import { ScheduleHour } from "../enums/ScheduleHour";

export interface UpdateScheduledActivity {
    id: number;
    startTime: ScheduleHour;
    endTime: ScheduleHour;
}