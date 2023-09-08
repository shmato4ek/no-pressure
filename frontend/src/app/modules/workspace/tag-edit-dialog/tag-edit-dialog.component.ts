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
import { UpdateActivityDialog } from 'src/app/models/activity/update-activity-dialog';
import { ActivityDTO } from 'src/app/models/activity/activity-dto';
import { Tag } from 'src/app/models/tag/tag';
import { UpdateTag } from 'src/app/models/tag/update-tag';

@Component({
  selector: 'task-add-dialog',
  templateUrl: './tag-edit-dialog.component.html',
  styleUrls: ['./tag-edit-dialog.component.css']
})
export class TagEditDialogComponent implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;
  tagName: string;
  tagId: number;
  tagColor: string;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ScheduleComponent>,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: Tag) { 
      this.tagName = data.name;
      this.tagId = data.id;
      this.tagColor = data.color;
    }

    ngOnInit(): void {
        this.dialogForm = this.formBuilder.group({
          tagName: [,{
            validators: [
              Validators.required,
              Validators.maxLength(30),
            ],
            updateOn:'change',
          }],
        });
        this.dialogForm.get('tagName')?.setValue(this.tagName);
    }

    save() {
      let activity: UpdateTag = {
        id: this.tagId,
        name: this.dialogForm.value.tagName,
        color: (<HTMLInputElement>document.getElementById("colorpicker")).value
      }
      this.dialogRef.close(activity);
    }
}