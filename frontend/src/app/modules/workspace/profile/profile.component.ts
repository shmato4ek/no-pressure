import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Subject, takeUntil } from 'rxjs';
import { ActivityDTO } from 'src/app/models/activity/activity-dto';
import { PlanDTO } from 'src/app/models/plan/plan-dto';
import { Statistic } from 'src/app/models/statistic/statistic';
import { Subscriptions } from 'src/app/models/subscriptions/subscriptions';
import { UserSubscription } from 'src/app/models/subscriptions/user-subscription';
import { UserDTO } from 'src/app/models/user/user-dto';
import { ActivityService } from 'src/app/services/activity.service';
import { PlanService } from 'src/app/services/plan.service';
import { RegistrationService } from 'src/app/services/registration.service';
import { UserService } from 'src/app/services/user.service';
import { SubscriptionDialogComponent } from '../subscriptions-dialog/subscription-dialog.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  public currentUser = {} as UserDTO;
  public statistic = {} as Statistic;
  public isAppear = false;

  private unsubscribe$ = new Subject<void>();

  constructor(
    private userService: UserService,
    private registrationService: RegistrationService,
    private activityService: ActivityService,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.registrationService
    .getUser()
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe((user) => {
      this.currentUser = user;
      this.activityService
        .getStatistic(this.currentUser.id)
        .subscribe((resp) => {
          this.statistic = resp;
        })
    });
    setTimeout(() => {
      this.showStat();
    }, 100)
  }
  showStat() {
    this.isAppear = true;
  }

  showSubscriptionsDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;

    dialogConfig.data = this.currentUser.id;

    const dialogRef = this.dialog.open(SubscriptionDialogComponent, dialogConfig);
  }
}
