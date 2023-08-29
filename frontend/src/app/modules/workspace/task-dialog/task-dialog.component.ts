import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NewActivity } from 'src/app/models/activity/new-activity';
import { ScheduleComponent } from '../schedule/schedule.component';
import { ActivityAddDialog } from 'src/app/models/activity/add-activity-dialog';
import { UpdateActivity } from 'src/app/models/activity/update-activity';
import { ActivityService } from 'src/app/services/activity.service';

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
}
