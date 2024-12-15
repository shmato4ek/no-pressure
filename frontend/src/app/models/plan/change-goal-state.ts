import { GoalState } from "../enums/GoalState";

export interface ChangeGoalState {
    id: number;
    state: GoalState;
}