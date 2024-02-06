import { TeamAccess } from "../enums/TeamAccess";

export interface SettingsCheckbox {
    userId: number;
    activityState: TeamAccess;
    userState: TeamAccess;
}