import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/login.service';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private router: Router,
              private loginService: LoginService) {}

  navigate() {
    if(this.loginService.areTokensExist()) {
      this.router.navigate(['./personal/schedule']);
    }
    else {
      this.router.navigate(['./auth']);
    }
  }
}
