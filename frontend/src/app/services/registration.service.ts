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

    override getResourceUrl(): string {
      return '/register';
  }

    public register(user: UserRegister) {
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
          this.user = resp.body?.user as UserDTO;
          return this.user;
        })
      );
    }
    
    public setTokens(token: Token) {
      if (token) {
        localStorage.setItem('accessToken', JSON.stringify(token));
      }
    }
}