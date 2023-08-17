import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { ActivityDTO } from 'src/app/models/activity/activity-dto';
import { ActivityService } from 'src/app/services/activity.service';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit{
  public userId = {} as number;
  public activities = [] as ActivityDTO[];
  private unsubscribe$ = new Subject<void>();

  constructor(
    private registrationService: RegistrationService,
    private activityService: ActivityService
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
        this.activityService.getAllActivities(user.id)
          .pipe(takeUntil(this.unsubscribe$))
          .subscribe((activities) => {
            this.activities = activities;
          })
      });
  }
}
