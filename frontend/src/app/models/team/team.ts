import { TeamTag } from "../tag/team-tag";
import { UserDTO } from "../user/user-dto";

export interface Team {
    id: number;
    name: string;
    date: Date;
    uniqId: string;
    authorId: number;
    users: UserDTO[];
    tags: TeamTag[];
}   