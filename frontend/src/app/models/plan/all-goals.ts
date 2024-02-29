import { GoalInfoDTO } from "./goal-info-dto";

export interface AllGoals {
    activeGoals: GoalInfoDTO[];
    closedGoals: GoalInfoDTO[];
}