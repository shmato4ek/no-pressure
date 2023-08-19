import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Subject, takeUntil } from 'rxjs';
import { ActivityDTO } from 'src/app/models/activity/activity-dto';
import { ActivityService } from 'src/app/services/activity.service';
import { RegistrationService } from 'src/app/services/registration.service';
import { TaskAddDialogComponent } from '../task-adding-dialog/task-add-dialog.component';
import { NewActivity } from 'src/app/models/activity/new-activity';

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
    private activityService: ActivityService,
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
        this.activityService.getAllActivities(user.id)
          .pipe(takeUntil(this.unsubscribe$))
          .subscribe((activities) => {
            this.activities = activities;
          })
      });
  }

  public showAddActivityDialog() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(TaskAddDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((activity) => {
      let newActivity: NewActivity = {
        userId: this.userId,
        name: activity.name,
        description: activity.description,
      };
        this.createActivity(newActivity);
    })
  }

  public createActivity(newActivity: NewActivity) {
    this.activityService.add(newActivity)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe();
    window.location.reload();
  }
}
