import { HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { UserRegister } from '../models/user/user-register';
import { UserDTO } from '../models/user/user-dto';
import { AuthUser } from '../models/user/auth-user';
import { Token } from '../models/token/token';
import { ResourceService } from './resource.service';

@Injectable({
    providedIn: 'root'
})

export class RegistrationService extends ResourceService<UserRegister> {
    private user: UserDTO = {} as UserDTO;
    private subUrl: string = '';

    setSubUrl(url: string) {
      this.subUrl = url;
    }

    getResourceUrl(): string {
      if (this.subUrl) {
        return this.subUrl;
      }
      return '';
    }

    public register(user: UserRegister) {
        this.setSubUrl('/register');
        return this.handleAuthResponse(
            this.add<UserRegister, AuthUser>(user)
        );
    }

    public getUser() {
      return this.getUserFromToken().pipe(
        map((resp) => {
          this.user = resp.body as UserDTO;
          return this.user;
        })
      );
    }

    private getUserFromToken() {
      return this.getFullRequest<UserDTO>('me');
    }

    private handleAuthResponse(
        observable: Observable<HttpResponse<AuthUser>>
      ) {
        return observable.pipe(
          map((resp) => {
            this.setTokens(resp.body?.token as unknown as Token);
            return resp.body?.user as UserDTO;
          })
        );
      }
    
    private setTokens(tokens: Token) {
        if (tokens && tokens.accessToken) {
            localStorage.setItem('accessToken', JSON.stringify(tokens.accessToken));
        }
    }
}