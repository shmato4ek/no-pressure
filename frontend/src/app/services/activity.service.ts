import { Injectable } from "@angular/core";
import { ResourceService } from "./resource.service";
import { ActivityDTO } from "../models/activity/activity-dto";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs";
import { UpdateStateActivity } from "../models/activity/update-state-activity";

@Injectable({
    providedIn: 'root',
})

export class ActivityService extends ResourceService<ActivityDTO> {
    override getResourceUrl(): string {
        return '/activity';
    }

    constructor (
        override httpClient: HttpClient,
    ) {
        super(httpClient);
    }

    public getAllActivities(id: number)
    {
        return this.getFullRequest<ActivityDTO[]>(`activity/user/${id}`)
            .pipe(
                map((resp) => {
                    return resp.body as ActivityDTO[];
                })
            );
    }

    public changeState(activity: UpdateStateActivity) {
        return this.patch(activity).subscribe();
    }
}