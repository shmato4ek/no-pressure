import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Subject, takeUntil } from 'rxjs';
import { ActivityDTO } from 'src/app/models/activity/activity-dto';
import { ActivityService } from 'src/app/services/activity.service';
import { RegistrationService } from 'src/app/services/registration.service';
import { TaskAddDialogComponent } from '../task-adding-dialog/task-add-dialog.component';
import { NewActivity } from 'src/app/models/activity/new-activity';
import { UpdateActivity } from 'src/app/models/activity/update-activity';
import { TaskUpdateDialogComponent } from '../task-update-dialog/task-update-dialog.component';

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

  public showUpdateActivityDialog(currentActivity: ActivityDTO) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.data = currentActivity;

    const dialogRef = this.dialog.open(TaskUpdateDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe((activity) => {
      let updateActivity: UpdateActivity = {
        id: currentActivity.id,
        name: activity.name,
        description: activity.description
      };
      this.updateActivity(updateActivity);
    })
  }

  public createActivity(newActivity: NewActivity) {
    this.activityService.add(newActivity)
      .subscribe();
    window.location.reload();
  }

  public updateActivity(updatedActivity: UpdateActivity) {
    this.activityService.update(updatedActivity)
      .subscribe();
    window.location.reload();
  }
}
