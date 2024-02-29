import { Injectable } from "@angular/core";
import { ResourceService } from "./resource.service";
import { ActivityDTO } from "../models/activity/activity-dto";
import { HttpClient, HttpErrorResponse, HttpResponse } from "@angular/common/http";
import { Observable, catchError, map, throwError } from "rxjs";
import { Tag } from "../models/tag/tag";
import { TagInfo } from "../models/tag/tag-info";
import { UpdateTag } from "../models/tag/update-tag";
import { environment } from "src/environments/environment";
import { Team } from "../models/team/team";
import { NewTeam } from "../models/team/new-team";
import { TeamInvitation } from "../models/team/team-invitation";
import { ChangeStatus } from "../models/team/change-status";
import { TeamWithSettings } from "../models/team/team-with-settings";
import { UpdateTeamSettings } from "../models/team/update-team-settings";
import { AddUsersToTeam } from "../models/team/add-users-to-team";
import { RemoveUserFromTeam } from "../models/team/remove-user-from-team";

@Injectable({
    providedIn: 'root',
})

export class TeamService extends ResourceService<Team> {
    override getResourceUrl(): string {
        return '/team';
    }

    constructor (
        override httpClient: HttpClient,
    ) {
        super(httpClient);
    }

    getTeamByUniqId(id: string) {
        return this.httpClient
            .get<Team>(`${environment.apiUrl}/team/${id}`, { observe: 'response' })
            .pipe(
                map((resp) => {
                    return resp.body as Team;
                })
            );
    }

    deleteTeam(id: number) {
        this.delete(id).subscribe();
    }

    removeUserFromTeam(request: RemoveUserFromTeam) {
        return this.httpClient
            .delete(`${environment.apiUrl}/team/user`, {body: request})
    }

    public getUsersTeams() {
        return this.httpClient
            .get<Team[]>(`${environment.apiUrl}/team/user`, { observe: 'response' })
            .pipe(
                map((resp) => {
                    return resp.body as Team[];
                })
            );
    }

    public getTeamById(id: number) {
        this.get(id).subscribe();
    }

    public createTeam(team: NewTeam) {
        this.add(team).subscribe();
    }

    public inviteToTeam(invitation: TeamInvitation) {
        return this.httpClient
            .post(`${environment.apiUrl}/team/invitation`, invitation, { observe: 'response' });
    }

    public changeTeamRequestStatus(request: ChangeStatus) {
        return this.httpClient
            .put(`${environment.apiUrl}/team/invitation`, request, {observe: 'response' })
    }

    public getTeamWithSettings(teamId: number) {
        return this.httpClient
            .get<TeamWithSettings>(`${environment.apiUrl}/team/settings/${teamId}`, { observe: 'response' })
            .pipe(
                map((resp) => {
                    return resp.body as TeamWithSettings;
                })
            );
    }

    public updateSettings(request: UpdateTeamSettings) {
        return this.httpClient
            .put(`${environment.apiUrl}/team/settings`, request, {observe: 'response' })
    }

    public addUsersToTeam(users: AddUsersToTeam) {
        return this.httpClient
            .post(`${environment.apiUrl}/team/invitation`, users, { observe: 'response' });
    }
}