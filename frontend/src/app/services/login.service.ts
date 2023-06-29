import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, Observable } from 'rxjs';
import { AuthUser } from '../models/user/auth-user';
import { UserLogin } from '../models/user/user-login';
import { Token } from '../models/token/token';
import { UserDTO } from '../models/user/user-dto';
import { ResourceService } from './resource.service';

@Injectable({
    providedIn: 'root',
})

export class LoginService extends ResourceService<UserLogin> {
    private user: UserDTO = {} as UserDTO;

    constructor(override httpClient: HttpClient, private router: Router) {
        super(httpClient);
      }

    override getResourceUrl(): string {
        return '/login';
    }

    public login(user: UserLogin) {
        return this.handleAuthResponse(this.add<UserLogin, AuthUser>(user));
    }

    public areTokensExist(): boolean {
        return (
          !!localStorage.getItem('accessToken') &&
          !!localStorage.getItem('refreshToken')
        );
    }
    
    public logOut() {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        this.router.navigate(['/login']);
    }

    private handleAuthResponse(
        observable: Observable<HttpResponse<AuthUser>>
      ) {
        return observable.pipe(
          map((resp) => {
            this.setTokens(resp.body?.token as unknown as Token);
            this.user = resp.body?.user as UserDTO;
            return this.user;
          })
        );
      }

      public setTokens(tokens: Token) {
        if (tokens && tokens.accessToken) {
          localStorage.setItem('accessToken', JSON.stringify(tokens.accessToken));
        }
      }
}