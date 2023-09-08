import { ScheduleHour } from "../enums/ScheduleHour";

export interface ActivityDTO {
    id: number;
    userId: number;
    name: string;
    description: string;
    color: string;
    startTime?: ScheduleHour;
    endTime?: ScheduleHour;
}