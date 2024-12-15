import { TeamAccess } from "../enums/TeamAccess";
import { TeamRole } from "../enums/TeamRole";
import { TeamTag } from "../tag/team-tag";
import { UserDTO } from "../user/user-dto";

export interface Team {
    id: number;
    name: string;
    date: Date;
    uniqId: string;
    color: string;
    addingUsers: TeamAccess;
    role: TeamRole;
    authorId: number;
    teamRequestId: number;
    users?: UserDTO[];
    tags?: TeamTag[];
}   