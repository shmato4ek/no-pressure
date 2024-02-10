import { Component, Inject, Input, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { ScheduleComponent } from '../schedule/schedule.component';
import { UpdateActivity } from 'src/app/models/activity/update-activity';
import { AddTaskToSchedule } from 'src/app/models/schedule/add-task-to-schedule';
import { ScheduleHour } from 'src/app/models/enums/ScheduleHour';
import { ScheduleActivity } from 'src/app/models/activity/schedule-activity';
import { ScheduleTime } from 'src/app/models/schedule/schedule-time';
import { CustomValidators } from '../../validators/custom-validators';

@Component({
  selector: 'task-add-dialog',
  templateUrl: './task-schedule-dialog.component.html',
  styleUrls: ['./task-schedule-dialog.component.css']
})
export class TaskScheduleDialogComponent implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;
  activityName: string;
  activityId: number;
  description: string;
  selectOptions: number[];
  schedule: ScheduleTime[];

  hasConflict = false;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ScheduleComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ScheduleActivity) { 
      this.activityName = data.name;
      this.activityId = data.id;
      this.description = data.description;
      this.schedule = data.schedule;
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
          }],
        },
        {
          validator: CustomValidators.schedullingValidation
        }
        );
    }

    save() {
      let activity: AddTaskToSchedule = {
        activityId: this.activityId,
        startTime: this.dialogForm.value.startTime,
        endTime: this.dialogForm.value.endTime,
      }
      this.dialogRef.close(activity);
    }

    scheduleValidator() {
      setTimeout(() => {
        this.scheduleValidation()
      }, 100)
    }

    after(value: ScheduleHour) : ScheduleHour {
      return value + 1;
    }

    before(value: ScheduleHour) : ScheduleHour{
      return value - 1;
    }

    scheduleValidation() {
      this.hasConflict = false;
      let startTime = this.dialogForm.get('startTime')?.value as ScheduleHour;
      let endTime = this.dialogForm.get('endTime')?.value as ScheduleHour;
      let flag = true;
      let hasConflict = false;

      for (let i = 0; i < this.schedule.length; i++) {
          if (this.schedule.find(h => h.hour == startTime && h.activity) != undefined) {
              hasConflict = true;
              flag = false;
          } else {
              if (startTime >= endTime || startTime == ScheduleHour.TwentyTwo) {
                  flag = false;
              } else {
                  startTime = this.after(startTime);
              }
          }

          if (this.schedule.find(h => h.hour == (endTime - 1) && h.activity) != undefined) {
              hasConflict = true;
              flag = false;
          } else {
              if (startTime >= endTime || endTime == ScheduleHour.Eight) {
                  flag = false;
              } else {
                  endTime = this.before(endTime);
              }
          }

          if (hasConflict || !flag) {
            i = this.schedule.length;
          }
      }

      if (hasConflict) {
          this.hasConflict = true;
      }
  }

  close() {
    this.dialogRef.close();
  }
}
