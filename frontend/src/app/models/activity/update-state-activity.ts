import { ActivityState } from "../enums/ActivityState";

export interface UpdateStateActivity
{
    id: number;
    state: ActivityState;
}