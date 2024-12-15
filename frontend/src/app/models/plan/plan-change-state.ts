import { PlanState } from "../enums/PlanState";

export interface PlanChangeState {
    id: number;
    state: PlanState;
}