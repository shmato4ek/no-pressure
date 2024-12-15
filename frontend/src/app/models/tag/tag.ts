import { ActivityDTO } from "../activity/activity-dto";

export interface Tag {
    id: number;
    userId: number;
    name: string;
    color: string;
    activities: ActivityDTO[];
}
