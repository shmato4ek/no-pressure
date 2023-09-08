import { Component, OnInit, Input } from '@angular/core';
import {Router} from '@angular/router';

import { UserDTO } from 'src/app/models/user/user-dto';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-w-header',
  templateUrl: './w-header.component.html',
  styleUrls: ['./w-header.component.css']
})
export class WorkspaceHeaderComponent {
  
  constructor(
    private router: Router,
    private loginService: LoginService) {}

  @Input() currentUser: UserDTO = {} as UserDTO;

  public redirectToProfile()
  {
    this.router.navigate(['./personal/profile']);
  }

  public logout() {
    this.loginService.logOut();
  }
}
