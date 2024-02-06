import { TeamPrivacyState } from "../enums/TeamPrivacyState";
import { UpdateSettings } from "./update-settings";

export interface UpdateTeamSettings {
    teamId: number;
    color: string;
    state: TeamPrivacyState;
    settings: UpdateSettings[];
}