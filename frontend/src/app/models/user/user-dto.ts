import { ActivityDTO } from "../activity/activity-dto";

export interface UserDTO {
    id: number;
    name: string;
    email: string;
    isNotificationsChecked: boolean;
    activities: ActivityDTO[];
}