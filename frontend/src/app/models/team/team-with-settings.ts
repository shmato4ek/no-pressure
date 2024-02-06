import { TeamPrivacyState } from "../enums/TeamPrivacyState";
import { TeamSettings } from "./team-settings";

export interface TeamWithSettings {
    id: number;
    color: string;
    state: TeamPrivacyState;
    settings: TeamSettings[];
}