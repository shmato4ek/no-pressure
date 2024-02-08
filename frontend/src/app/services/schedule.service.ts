import { Injectable } from "@angular/core";
import { ResourceService } from "./resource.service";
import { ActivityDTO } from "../models/activity/activity-dto";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs";
import { PlanDTO } from "../models/plan/plan-dto";
import { Schedule } from "../models/schedule/schedule";
import { AddTaskToSchedule } from "../models/schedule/add-task-to-schedule";
import { TeamSchedule } from "../models/schedule/team-schedule";

@Injectable({
    providedIn: 'root',
})

export class ScheduleService extends ResourceService<Schedule> {
    override getResourceUrl(): string {
        return '/schedule';
    }

    constructor (
        override httpClient: HttpClient,
    ) {
        super(httpClient);
    }
    
    public getSchedule(userId: number) {
        return this.getFullRequest<Schedule>(`schedule/${userId}`)
            .pipe(
                map((resp) => {
                    return resp.body as Schedule;
                })
            );
    }

    public getTeamSchedule(teamId: number) {
        return this.getFullRequest<TeamSchedule>(`schedule/team/${teamId}`)
            .pipe(
                map((resp) => {
                    return resp.body as TeamSchedule;
                })
            );
    }

    public getTeamScheduleWithActivities(teamId: number) {
        return this.getFullRequest<Schedule>(`schedule/team/schedule/${teamId}`)
            .pipe(
                map((resp) => {
                    return resp.body as Schedule;
                })
            );
    }

    public addActivityToSchedule(activity: AddTaskToSchedule) {
        return this.add(activity)
            .subscribe();
    }

    public removeActivityFromSchedule(activityId: number) {
        return this.delete(activityId)
            .subscribe();
    }
}