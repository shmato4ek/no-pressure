import { TeamRequestState } from "../enums/TeamRequestState";

export interface ChangeStatus {
    id: number;
    status: TeamRequestState;
}