import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Subject, takeUntil } from 'rxjs';
import { ActivityDTO } from 'src/app/models/activity/activity-dto';
import { PlanDTO } from 'src/app/models/plan/plan-dto';
import { ActivityService } from 'src/app/services/activity.service';
import { PlanService } from 'src/app/services/plan.service';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-plans',
  templateUrl: './plans.component.html',
  styleUrls: ['./plans.component.css']
})
export class PlansComponent implements OnInit{
  public userId = {} as number;
  public plans = [] as PlanDTO[];
  private unsubscribe$ = new Subject<void>();

  constructor(
    private registrationService: RegistrationService,
    private planService: PlanService,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
      this.getUserId();
  }

  public getUserId() {
    this.registrationService
      .getUser()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((user) => {
        this.userId = user.id;
        this.planService
          .getAllNoGoalActivities(user.id)
          .pipe(takeUntil(this.unsubscribe$))
          .subscribe((plans) => {
            this.plans = plans;
          });
      });
  }
}
