import { Injectable } from "@angular/core";
import { ResourceService } from "./resource.service";
import { UserDTO } from "../models/user/user-dto";
import { SubscribeRequest } from "../models/subscriptions/subscribe-request";
import { Subscriptions } from "../models/subscriptions/subscriptions";
import { catchError, map } from "rxjs";
import { UserShared } from "../models/user/user-shared";
import { Settings } from "../models/settings/settings";
import { UpdateUser } from "../models/user/update-user";
import { ChangePassword } from "../models/user/change-password";
import { environment } from "src/environments/environment";
import { Notification } from "../models/notifications/notification";

@Injectable({
    providedIn: 'root',
})

export class UserService extends ResourceService<UserDTO> {
    override getResourceUrl(): string {
        return "/user/subscriptions";
    }
    
    getSubscriptions(userId: number) {
        return this.getFullRequest<Subscriptions>(`user/subscriptions/${userId}`)
        .pipe(
            map((resp) => {
                return resp.body as Subscriptions;
            })
        );
    }

    public getUserByEmail(email: string) {
        return this.getFullRequest<UserShared>(`user/${email}`)
        .pipe(
            map((resp) => {
                return resp.body as UserShared;
            })
        )
      }

    public subscribe(subscribe: SubscribeRequest) {
        this.add(subscribe).subscribe();
    }

    public unsubscribe(userId: number) {
        this.delete(userId).subscribe();
    }

    public updateUser(user: UpdateUser) {
        return this.httpClient
            .put<UserDTO>(`${environment.apiUrl}/user`, user, {observe: 'response'});
    }

    public changePassword(changePassword: ChangePassword) {
        return this.httpClient
            .put<UserDTO>(`${environment.apiUrl}/user/password`, changePassword, {observe: 'response'});
    }

    public getNotifications() {
        return this.getFullRequest<Notification[]>(`user/notifications`)
        .pipe(
            map((resp) => {
                return resp.body as Notification[];
            })
        );
    }

    public checkNotification(id: number) {
        return this.httpClient
        .put(`${environment.apiUrl}/user/notifications/${id}`, {observe: 'response'});
    }
}