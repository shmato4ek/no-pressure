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
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  public userId = {} as number;
  public plans = [] as PlanDTO[];
  private unsubscribe$ = new Subject<void>();

  public p = 60 as number;

  constructor(
    private registrationService: RegistrationService,
    private planService: PlanService,
    public dialog: MatDialog
  ) { }


}
