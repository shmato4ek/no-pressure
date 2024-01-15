import { Injectable } from "@angular/core";
import { ResourceService } from "./resource.service";
import { UserDTO } from "../models/user/user-dto";
import { SubscribeRequest } from "../models/subscriptions/subscribe-request";
import { Subscriptions } from "../models/subscriptions/subscriptions";
import { map } from "rxjs";
import { UserShared } from "../models/user/user-shared";

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

}