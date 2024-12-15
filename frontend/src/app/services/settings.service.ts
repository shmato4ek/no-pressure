import { Injectable } from "@angular/core";
import { ResourceService } from "./resource.service";
import { UserDTO } from "../models/user/user-dto";
import { SubscribeRequest } from "../models/subscriptions/subscribe-request";
import { Subscriptions } from "../models/subscriptions/subscriptions";
import { map } from "rxjs";
import { UserShared } from "../models/user/user-shared";
import { Settings } from "../models/settings/settings";

@Injectable({
    providedIn: 'root',
})

export class SettingsService extends ResourceService<Settings> {
    override getResourceUrl(): string {
        return "/user/settings";
    }

    public updateSettings(settings: Settings) {
        return this.update(settings).subscribe();
    }

    public getSettings() {
        return this.getFullRequest<Settings>('user/settings').pipe(
            map((resp) => {
              return resp.body as Settings;
            })
          );
    }
}