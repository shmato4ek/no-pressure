import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { LoginService } from '../services/login.service';

@Injectable({
  providedIn: 'root'
})
export class TokenExistsGuard implements CanActivate {
  constructor(
    private loginService : LoginService,
    private router : Router) { }
  canActivate ( ) : boolean {
    if(!this.loginService.areTokensExist()) {
      this.router.navigate(['/login']);
    }
    return this.loginService.areTokensExist();
  }
}
