import { Injectable } from "@angular/core";
import { ResourceService } from "./resource.service";
import { ActivityDTO } from "../models/activity/activity-dto";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs";
import { PlanDTO } from "../models/plan/plan-dto";

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
}