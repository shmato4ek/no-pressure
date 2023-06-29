import { Component, Input, ViewChild } from '@angular/core';
import { MatMenuTrigger } from '@angular/material/menu';

import { UserDTO } from 'src/app/models/user/user-dto';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-u-navbar',
  templateUrl: './u-navbar.component.html',
  styleUrls: ['./u-navbar.component.css']
})
export class UserNavbarComponent {

  @Input() public user: UserDTO = {} as UserDTO
  @ViewChild(MatMenuTrigger)
  contextMenu?: MatMenuTrigger;
  constructor(private loginService: LoginService) {}
  onContextMenu(event: MouseEvent) {
    event.preventDefault();
    if(this.contextMenu?.menu)
    {
      this.contextMenu?.menu.focusFirstItem('mouse');
    }
    this.contextMenu?.openMenu();
  }

  onLogOut() {
    this.loginService.logOut();
  }
}
