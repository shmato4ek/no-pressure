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
  
  public isTagActive: boolean = false;
  public isSettingColor: boolean = false;
  public isRepeatable: boolean = false;

  colorInput: HTMLInputElement;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ScheduleComponent>) {
      this.colorInput = document.getElementById('colorpicker') as HTMLInputElement;
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
          }],
          activityTag: [,{
            validators: [
              Validators.maxLength(10)
            ]
          }],
        });
    }

    save() {
      let activity: ActivityAddDialog = {
        name: this.dialogForm.value.activityName,
        description: this.dialogForm.value.activityDescription,
        tag: this.dialogForm.value.activityTag,
        isRepeatable: this.isRepeatable,
        color: this.colorInput.value
      }
      this.dialogRef.close(activity);
    }

    changeTagState() {
      this.isTagActive = !this.isTagActive;
    }

    changeSetColorState() {
      this.isSettingColor = !this.isSettingColor;
    }
}
