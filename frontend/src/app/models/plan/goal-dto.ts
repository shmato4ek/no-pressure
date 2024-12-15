import { NewActivity } from "../activity/new-activity";
import { NewTag } from "../tag/new-tag";

export interface GoalDTO {
    id: number;
    userId: number;
    name: string;
    tag: NewTag;
    activities: NewActivity[];
}
