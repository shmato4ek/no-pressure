import { ActivityDTO } from "../activity/activity-dto";
import { PlanState } from "../enums/PlanState";

export interface PlanDTO {
    id: number;
    userId: number;
    name: string;
    activities: ActivityDTO[];
    state: PlanState;
}