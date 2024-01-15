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
import { ActivatedRoute, Router } from '@angular/router';
import { SubscribeRequest } from 'src/app/models/subscriptions/subscribe-request';

@Component({
  selector: 'app-shared-profile',
  templateUrl: './shared-profile.component.html',
  styleUrls: ['./shared-profile.component.css']
})
export class SharedProfileComponent implements OnInit {
  public currentUser = {} as UserDTO;
  public statistic = {} as Statistic;
  public isAppear = false;
  public userId = 0;

  public isFollowed = false;

  private unsubscribe$ = new Subject<void>();

  constructor(
    private registrationService: RegistrationService,
    private userService: UserService,
    private router: Router,
    public dialog: MatDialog,
    private activateRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.userService
      .getUserByEmail(this.activateRoute.snapshot.params['id'])
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((resp) => {
        this.currentUser = resp.user;
        this.statistic = resp.statistic;
        this.isFollowed = resp.isFollowed;

        this.registrationService
          .getUser()
          .subscribe((resp) => {
            this.checkSameUser(resp.id);
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

    dialogRef.afterClosed().subscribe(() => {
      window.location.reload();
    })
  }

  subscribe() {
    let subscribe: SubscribeRequest = {
      followingId: this.currentUser.id
    }

    this.userService.subscribe(subscribe);

    window.location.reload();
  }

  unsubscribe() {
    this.userService.unsubscribe(this.currentUser.id);

    window.location.reload();
  }

  checkSameUser(userBasicId: number) {
    if (this.currentUser.id == userBasicId) {
      this.router.navigate(['profile']);
    }
  }
}
