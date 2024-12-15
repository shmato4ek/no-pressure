import { ActivityDTO } from "../activity/activity-dto";
import { AuthType } from "../enums/AuthType";
import { TeamInfo } from "../team/team-info";

export interface UserDTO {
    id: number;
    name: string;
    email: string;
    isNotificationsChecked: boolean;
    registrationDate: string;
    activities: ActivityDTO[];
    teams?: TeamInfo[];
    authType: AuthType;
}