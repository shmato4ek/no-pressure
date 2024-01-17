import { SettingsPrivacy } from "../enums/settings-privacy";

export interface Settings {
    statistic: SettingsPrivacy;
    activities: SettingsPrivacy;
}