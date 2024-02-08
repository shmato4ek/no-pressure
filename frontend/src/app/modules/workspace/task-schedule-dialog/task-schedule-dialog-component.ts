import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { ScheduleComponent } from '../schedule/schedule.component';
import { UpdateActivity } from 'src/app/models/activity/update-activity';
import { AddTaskToSchedule } from 'src/app/models/schedule/add-task-to-schedule';

@Component({
  selector: 'task-add-dialog',
  templateUrl: './task-schedule-dialog.component.html',
  styleUrls: ['./task-schedule-dialog.component.css']
})
export class TaskScheduleDialogComponent implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;
  activityName: string;
  activityId: number;
  selectOptions: number[];

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ScheduleComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UpdateActivity) { 
      this.activityName = data.name;
      this.activityId = data.id;
      this.selectOptions = [6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23];
    }

    ngOnInit(): void {
        this.dialogForm = this.formBuilder.group({
          startTime: [,{
            validators: [
              Validators.required,
            ],
            updateOn:'change',
          }],
          endTime: [,{
            validators: [
              Validators.required,
            ],
            updateOn:'change',
          }]
        });
    }

    save() {
      let activity: AddTaskToSchedule = {
        activityId: this.activityId,
        startTime: this.dialogForm.value.startTime,
        endTime: this.dialogForm.value.endTime,
      }
      this.dialogRef.close(activity);
    }
}
