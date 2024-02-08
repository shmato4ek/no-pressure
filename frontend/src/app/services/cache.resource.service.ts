import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParams,
  HttpResponse,
} from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError, takeUntil } from 'rxjs/operators';
import { CacheService } from './cache.service';
import { ResourceService } from './resource.service';
import { RegistrationService } from './registration.service';
import { UserDTO } from '../models/user/user-dto';
import { Schedule } from '../models/schedule/schedule';
import { ScheduleService } from './schedule.service';

@Injectable({
    providedIn: 'root'
})
export class CacheResourceService {
    private unsubscribe$ = new Subject<void>();

    constructor(private cacheService: CacheService,
                private registrationService: RegistrationService,
                private scheduleService: ScheduleService) { }
    
    private me_key = "me";
    private currentUser = {} as UserDTO | undefined;
    private currentSchedule = {} as Schedule | undefined;

    private getScheduleKey(id: number) {
        return "schedule_" + id.toString();
    }
    
    public async getUser() {
        const cache = this.cacheService.get(this.me_key);
        if (cache != undefined) {
            this.currentUser = cache[0] as UserDTO;
        } else {
            this.currentUser = await this.registrationService
                .getUser().toPromise();

            let dataArray = [] as any[];
            dataArray.push(this.currentUser);
            this.cacheService.set(this.me_key, dataArray);
        }

        return this.currentUser;
    }

    public async getSchedule(id: number) {
        let key = this.getScheduleKey(id);
        const cache = this.cacheService.get(key);
        if(cache != undefined) {
            this.currentSchedule = cache[0] as Schedule;
        } else {
            this.currentSchedule = await this.scheduleService
                .getTeamScheduleWithActivities(id)
                .toPromise();

            let dataArray = [] as any[];
            dataArray.push(this.currentSchedule);
            this.cacheService.set(key, dataArray);
        }

        return this.currentSchedule;
    }
}