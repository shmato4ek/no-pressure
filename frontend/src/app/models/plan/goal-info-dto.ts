import { ActivityDTO } from "../activity/activity-dto";
import { NewActivity } from "../activity/new-activity";
import { NewTag } from "../tag/new-tag";

export interface GoalInfoDTO {
    id: number;
    name: string;
    activeActivities: ActivityDTO[];
    doneActivities: ActivityDTO[];
    progress: number;
    doneTasksAmmount: number;
    allTasksAmmount: number;
}