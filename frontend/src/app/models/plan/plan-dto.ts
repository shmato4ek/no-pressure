import { ActivityDTO } from "../activity/activity-dto";

export interface PlanDTO {
    id: number;
    userId: number;
    name: string;
    activities: ActivityDTO[];
    isGoal: boolean;
}