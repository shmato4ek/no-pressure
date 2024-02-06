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

@Injectable({
    providedIn: 'root'
})
export class CacheResourceService {
    private unsubscribe$ = new Subject<void>();

    constructor(private cacheService: CacheService,
                private registrationService: RegistrationService) { }
    
    private me_key = "me";
    private currentUser = {} as UserDTO | undefined;
    
    public async getUser() {
        console.log(`Start of getUser()`)
        const cache = this.cacheService.get(this.me_key);
        if (cache != undefined) {
            console.log(`Cache is not empty. Cache: ${cache[0].name}`)
            this.currentUser = cache[0] as UserDTO;
        } else {
            console.log(`Cache is empty`)
            this.currentUser = await this.registrationService
                .getUser().toPromise();

            let dataArray = [] as any[];
            dataArray.push(this.currentUser);
            this.cacheService.set(this.me_key, dataArray);
        }

        console.log(`End of getUser(). User: ${this.currentUser?.name}`)
        return this.currentUser;
    }
}