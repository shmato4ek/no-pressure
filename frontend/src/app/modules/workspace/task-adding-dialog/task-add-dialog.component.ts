import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NewActivity } from 'src/app/models/activity/new-activity';
import { ScheduleComponent } from '../schedule/schedule.component';
import { ActivityAddDialog } from 'src/app/models/activity/add-activity-dialog';

@Component({
  selector: 'task-add-dialog',
  templateUrl: './task-add-dialog.component.html',
  styleUrls: ['./task-add-dialog.component.css']
})
export class TaskAddDialogComponent implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ScheduleComponent>)
    { }

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
    }

    save() {
      let activity: ActivityAddDialog = {
        name: this.dialogForm.value.activityName,
        description: this.dialogForm.value.activityDescription,
      }
      this.dialogRef.close(activity);
    }
}
