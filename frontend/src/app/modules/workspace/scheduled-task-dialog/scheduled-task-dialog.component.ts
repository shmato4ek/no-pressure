import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NewActivity } from 'src/app/models/activity/new-activity';
import { ScheduleComponent } from '../schedule/schedule.component';
import { ActivityAddDialog } from 'src/app/models/activity/add-activity-dialog';
import { TagService } from 'src/app/services/tag.service';
import { TagInfo } from 'src/app/models/tag/tag-info';
import { RegistrationService } from 'src/app/services/registration.service';
import { ActivityDTO } from 'src/app/models/activity/activity-dto';
import { UpdateScheduledActivity } from 'src/app/models/activity/update-scheduled-activity';
import { ScheduleService } from 'src/app/services/schedule.service';
import { UpdateStateActivity } from 'src/app/models/activity/update-state-activity';
import { ActivityService } from 'src/app/services/activity.service';
import { ScheduleActivity } from 'src/app/models/activity/schedule-activity';
import { ScheduleTime } from 'src/app/models/schedule/schedule-time';
import { ScheduleHour } from 'src/app/models/enums/ScheduleHour';
import { ScheduledActivity } from 'src/app/models/activity/scheduled-activity';
import { ActivityState } from 'src/app/models/enums/ActivityState';
import { CustomValidators } from '../../validators/custom-validators';

@Component({
  selector: 'task-add-dialog',
  templateUrl: './scheduled-task-dialog.component.html',
  styleUrls: ['./scheduled-task-dialog.component.css']
})
export class ScheduledTaskDialogComponent implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;

  selectOptions: number[];
  activityName: string;
  activityId: number;
  schedule: ScheduleTime[];
  startTime: ScheduleHour | undefined;
  endTime: ScheduleHour | undefined;
  state: ActivityState;

  hasConflict = false;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ScheduleComponent>,
    private scheduleService: ScheduleService,
    private activityService: ActivityService,
    @Inject(MAT_DIALOG_DATA) public data: ScheduledActivity) {
      this.activityName = data.name;
      this.activityId = data.id;
      this.schedule = data.schedule;
      this.startTime = data.startTime;
      this.endTime = data.endTime;
      this.state = data.state;
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
      },
      {
        validator: CustomValidators.schedullingValidation
      });
    }

    save() {
      let activity: UpdateScheduledActivity = {
        id: this.activityId,
        startTime: this.dialogForm.value.startTime,
        endTime: this.dialogForm.value.endTime,
      }

      this.dialogRef.close(activity);
    }

    delete() {
      this.scheduleService.removeActivityFromSchedule(this.activityId);
      window.location.reload();
    }

    changeState(currentState: number) {
      let activity: UpdateStateActivity = {
        id: this.activityId,
        state: currentState
      }
      
      this.activityService.changeState(activity);
      window.location.reload();
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
              if (this.schedule.find(h => h.hour == startTime)?.activity?.id != this.activityId) {
                hasConflict = true;
                flag = false;
              }
          } else {
              if (startTime >= endTime || startTime == ScheduleHour.TwentyTwo) {
                  flag = false;
              } else {
                  startTime = this.after(startTime);
              }
          }

          if (this.schedule.find(h => h.hour == (endTime - 1) && h.activity) != undefined) {
              if (this.schedule.find(h => h.hour == (endTime - 1))?.activity?.id != this.activityId) {
                hasConflict = true;
                flag = false;
              }
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
