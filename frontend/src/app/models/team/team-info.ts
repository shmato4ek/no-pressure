import { TeamAccess } from "../enums/TeamAccess";
import { TeamTag } from "../tag/team-tag";

export interface TeamInfo {
    id: number;
    name: string;
    date: Date;
    color: string;
    addingActivities: TeamAccess;
    tags: TeamTag[];
}