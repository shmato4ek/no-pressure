import { ScheduleHour } from "../enums/ScheduleHour";

export interface AddTaskToSchedule {
    activityId: number;
    startTime: ScheduleHour;
    endTime: ScheduleHour;
}
