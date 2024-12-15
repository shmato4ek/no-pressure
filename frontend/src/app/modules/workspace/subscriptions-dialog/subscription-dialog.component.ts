import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { ProfileComponent } from "../profile/profile.component";
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from "@angular/material/dialog";
import { Subscriptions } from "src/app/models/subscriptions/subscriptions";
import { UserSubscription } from "src/app/models/subscriptions/user-subscription";
import { UserService } from "src/app/services/user.service";
import { Router } from "@angular/router";

@Component({
  selector: 'subscription-dialog',
  templateUrl: './subscription-dialog.component.html',
  styleUrls: ['./subscription-dialog.component.css']
})

export class SubscriptionDialogComponent{
  dialogForm: FormGroup = {} as FormGroup;

  followers = [] as UserSubscription[];
  followings = [] as UserSubscription[];

  isFollowers = true;

  constructor(
    private userService: UserService,
    private dialogRef: MatDialogRef<ProfileComponent>,
    public dialog: MatDialog,
    private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: number) { 
      this.userService
        .getSubscriptions(data)
        .subscribe((resp) => {
          this.followers = resp.followers,
          this.followings = resp.followings
        })
    }

    toggleSubscriptions(type: string) {
      if (type == 'followers') {
        this.isFollowers = true;
      }
      else {
        this.isFollowers = false;
      }
    }

    redirect(user: UserSubscription) {
      let path = btoa(user.user.email);
      this.router.navigate([`profile/${path}`]);
      this.dialogRef.close(true);
    }

    close() {
      this.dialogRef.close();
    }
}
