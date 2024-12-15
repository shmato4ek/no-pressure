import { TeamAccess } from "../enums/TeamAccess";

export interface UpdateSettings {
    id: number;
    addingUsers: TeamAccess;
    addingActivities: TeamAccess;
}