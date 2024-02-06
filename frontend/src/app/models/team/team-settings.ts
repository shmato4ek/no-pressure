import { TeamAccess } from "../enums/TeamAccess";

export interface TeamSettings {
    id: number;
    userId: number;
    userName: string;
    addingUsers: TeamAccess;
    addingActivities: TeamAccess;
}