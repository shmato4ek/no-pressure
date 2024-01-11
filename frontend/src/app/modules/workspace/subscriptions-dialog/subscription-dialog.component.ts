import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { ProfileComponent } from "../profile/profile.component";
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from "@angular/material/dialog";
import { Subscriptions } from "src/app/models/subscriptions/subscriptions";
import { UserSubscription } from "src/app/models/subscriptions/user-subscription";

@Component({
  selector: 'subscription-dialog',
  templateUrl: './subscription-dialog.component.html',
  styleUrls: ['./subscription-dialog.component.css']
})

export class SubscriptionDialogComponent{
  dialogForm: FormGroup = {} as FormGroup;

  followers: UserSubscription[];
  followings: UserSubscription[];

  isFollowers = true;

  constructor(
    private dialogRef: MatDialogRef<ProfileComponent>,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: Subscriptions) { 
      this.followers = data.followers;
      this.followings = data.followings;
    }

    toggleSubscriptions(type: string) {
      if (type == 'followers') {
        this.isFollowers = true;
      }
      else {
        this.isFollowers = false;
      }
    }
}
