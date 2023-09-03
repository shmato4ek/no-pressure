import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogConfig, MatDialogRef, MatDialog } from '@angular/material/dialog';
import { ScheduleComponent } from '../schedule/schedule.component';
import { ActivityAddDialog } from 'src/app/models/activity/add-activity-dialog';
import { UpdateActivity } from 'src/app/models/activity/update-activity';
import { ActivityService } from 'src/app/services/activity.service';
import { TaskScheduleDialogComponent } from '../task-schedule-dialog.ts/task-schedule-dialog-component';
import { ScheduleService } from 'src/app/services/schedule.service';
import { AddTaskToSchedule } from 'src/app/models/schedule/add-task-to-schedule';

@Component({
  selector: 'task-add-dialog',
  templateUrl: './task-dialog.component.html',
  styleUrls: ['./task-dialog.component.css']
})
export class TaskDialogComponent implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;
  activityName: string;
  activityDescription: string;
  activityId: number;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ScheduleComponent>,
    private activityService: ActivityService,
    private scheduleService: ScheduleService,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: UpdateActivity) { 
      this.activityName = data.name;
      this.activityDescription = data.description;
      this.activityId = data.id;
    }

    ngOnInit(): void {
        this.dialogForm = this.formBuilder.group({
          activityName: [,{
            validators: [
              Validators.required,
              Validators.maxLength(30),
            ],
            updateOn:'change',
          }],
          activityDescription: [,{
            validators: [
              Validators.maxLength(50),
            ],
            updateOn:'change',
          }]
        });
        this.dialogForm.get('activityName')?.setValue(this.activityName);
        this.dialogForm.get('activityDescription')?.setValue(this.activityDescription);
    }

    save() {
      let activity: ActivityAddDialog = {
        name: this.dialogForm.value.activityName,
        description: this.dialogForm.value.activityDescription,
      }
      this.dialogRef.close(activity);
    }

    delete() {
      this.activityService.delete(this.activityId).subscribe();
      window.location.reload();
    }

    public showScheduleActivityDialog() {
      const dialogConfig = new MatDialogConfig();
  
      dialogConfig.autoFocus = true;
      
      let activity: UpdateActivity = {
        name: this.activityName,
        id: this.activityId,
        description: this.activityDescription
      }

      dialogConfig.data = activity;
  
      const dialogRef = this.dialog.open(TaskScheduleDialogComponent, dialogConfig);
  
      dialogRef.afterClosed().subscribe((activity) => {
        let scheduledActivity: AddTaskToSchedule = {
          activityId: this.activityId,
          startTime: activity.startTime,
          endTime: activity.endTime,
        };
        this.scheduleActivity(scheduledActivity);
      })
    }

    public scheduleActivity(activity: AddTaskToSchedule) {
      this.scheduleService.addActivityToSchedule(activity);
      window.location.reload();
    }

}
