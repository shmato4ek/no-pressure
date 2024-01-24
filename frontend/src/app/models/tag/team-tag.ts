import { ActivityDTO } from "../activity/activity-dto";

export interface TeamTag {
    id: number;
    name: string;
    color: string;
    activities: ActivityDTO[];
    activitiesDone: number;
    activitiesAll: number;
}