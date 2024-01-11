import { Injectable } from "@angular/core";
import { ResourceService } from "./resource.service";
import { UserDTO } from "../models/user/user-dto";
import { SubscribeRequest } from "../models/subscriptions/subscribe-request";
import { Subscriptions } from "../models/subscriptions/subscriptions";
import { map } from "rxjs";

@Injectable({
    providedIn: 'root',
})

export class UserService extends ResourceService<UserDTO> {
    override getResourceUrl(): string {
        return "";
    }
    
    getSubscriptions(userId: number) {
        return this.getFullRequest<Subscriptions>(`user/subscriptions/${userId}`)
        .pipe(
            map((resp) => {
                return resp.body as Subscriptions;
            })
        );
    }

}