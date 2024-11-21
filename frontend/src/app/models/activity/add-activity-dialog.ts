import { ScheduleHour } from "../enums/ScheduleHour";

export interface ActivityAddDialog {
    name: string;
    description: string;
    tag?: string;
    isRepeatable: boolean;
    color: string;
    teamId: number;
    directiveTerm: ScheduleHour;
    priority: number;
    delayCoefficient: number;
    duration: number;
}