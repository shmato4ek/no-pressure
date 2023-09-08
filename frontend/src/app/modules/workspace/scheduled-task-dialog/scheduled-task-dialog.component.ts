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

@Component({
  selector: 'task-add-dialog',
  templateUrl: './scheduled-task-dialog.component.html',
  styleUrls: ['./scheduled-task-dialog.component.css']
})
export class ScheduledTaskDialogComponent implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;

  selectOptions: number[];
  currentActivity: ActivityDTO;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ScheduleComponent>,
    private scheduleService: ScheduleService,
    @Inject(MAT_DIALOG_DATA) public data: ActivityDTO) {
      this.currentActivity = data;
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
      let activity: UpdateScheduledActivity = {
        id: this.currentActivity.id,
        startTime: this.dialogForm.value.startTime,
        endTime: this.dialogForm.value.endTime,
      }

      this.dialogRef.close(activity);
    }

    delete() {
      this.scheduleService.removeActivityFromSchedule(this.currentActivity.id);
      window.location.reload();
    }
}
