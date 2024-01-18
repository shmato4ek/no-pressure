import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { ProfileComponent } from "../profile/profile.component";
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from "@angular/material/dialog";
import { Subscriptions } from "src/app/models/subscriptions/subscriptions";
import { UserSubscription } from "src/app/models/subscriptions/user-subscription";
import { UserService } from "src/app/services/user.service";
import { Router } from "@angular/router";
import { Notification } from "src/app/models/notifications/notification";
import { WorkspaceHeaderComponent } from "../workspace-header/w-header.component";

@Component({
  selector: 'notifications-dialog',
  templateUrl: './notifications-dialog.component.html',
  styleUrls: ['./notifications-dialog.component.css']
})

export class NotificationsDialogComponent{
  notifications = [] as Notification[];

  constructor(
    private userService: UserService,
    private dialogRef: MatDialogRef<WorkspaceHeaderComponent>,
    public dialog: MatDialog,
    private router: Router) { 
      this.userService
        .getNotifications()
        .subscribe((resp) => {
          this.notifications = resp;
          console.log(this.notifications[0].date)
        })
    }

    redirect(id: number, url?: string) {
      if(url == null) {
        return;
      }
      this.checkNotification(id);
      this.router.navigate([`${url}`]);
      this.dialogRef.close();
    }

    checkNotification(id: number) {
      this.userService
        .checkNotification(id)
        .subscribe();
    }
}
