import { Injectable } from "@angular/core";
import { ResourceService } from "./resource.service";
import { ActivityDTO } from "../models/activity/activity-dto";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs";
import { PlanDTO } from "../models/plan/plan-dto";
import { NewPlanDTO } from "../models/plan/new-plan";
import { PlanChangeState } from "../models/plan/plan-change-state";
import { UpdatePlanDTO } from "../models/plan/plan-update";
import { GoalDTO } from "../models/plan/goal-dto";
import { GoalInfoDTO } from "../models/plan/goal-info-dto";
import { AllGoals } from "../models/plan/all-goals";
import { ChangeGoalState } from "../models/plan/change-goal-state";

@Injectable({
    providedIn: 'root',
})

export class PlanService extends ResourceService<PlanDTO> {
    override getResourceUrl(): string {
        return '/plan';
    }

    constructor (
        override httpClient: HttpClient,
    ) {
        super(httpClient);
    }

    public getAllNoGoalActivities(id: number) {
        return this.getFullRequest<PlanDTO[]>(`plan/${id}`)
            .pipe(
                map((resp) => {
                    return resp.body as PlanDTO[];
                })
            );
    }

    public getAllGoals(id: number) {
        return this.getFullRequest<AllGoals>(`plan/goals/${id}`)
            .pipe(
                map((resp) => {
                    return resp.body as AllGoals;
                })
            );
    }

    public createPlan(newPlan: NewPlanDTO) {
        return this.add(newPlan).subscribe();
    }

    public changeGoalState(goal: ChangeGoalState) {
        return this.patch<ChangeGoalState>(goal, `plan/goal/state`).subscribe();
    }

    public convertToGoal(goal: GoalDTO) {
        return this.patch<GoalDTO>(goal, `plan/goal`).subscribe();
    }

    public updatePlan(updatedPlan: UpdatePlanDTO) {
        return this.update(updatedPlan).subscribe();
    }

    public deletePlan(userId: number) {
        return this.delete(userId).subscribe();
    }
}