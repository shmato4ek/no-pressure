import { Component, OnInit, Input } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import {Router} from '@angular/router';

import { UserDTO } from 'src/app/models/user/user-dto';
import { LoginService } from 'src/app/services/login.service';
import { NotificationsDialogComponent } from '../notifications-dialog/notifications-dialog.component';

@Component({
  selector: 'app-w-header',
  templateUrl: './w-header.component.html',
  styleUrls: ['./w-header.component.css']
})
export class WorkspaceHeaderComponent {

  constructor(
    private router: Router,
    private loginService: LoginService,
    public dialog: MatDialog) {}

  @Input() currentUser: UserDTO = {} as UserDTO;

  showNotificationsDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;

    dialogConfig.data = this.currentUser.id;

    const dialogRef = this.dialog.open(NotificationsDialogComponent, dialogConfig);

    
    dialogRef.afterClosed().subscribe(() => {
      window.location.reload();
    })
  }

  public redirectToProfile() {
    this.router.navigate(['./profile']);
  }

  public redirectToSettings() {
    this.router.navigate(['./settings']);
  }

  public logout() {
    this.loginService.logOut();
  }
}
