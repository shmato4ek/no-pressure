import { ActivityDTO } from "../activity/activity-dto";
import { NewActivity } from "../activity/new-activity";
import { NewTag } from "../tag/new-tag";

export interface GoalInfoDTO {
    id: number;
    name: string;
    activities: ActivityDTO[];
    progress: number;
    doneTasksAmmount: number;
    allTasksAmmount: number;
}