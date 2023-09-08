import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NewActivity } from 'src/app/models/activity/new-activity';
import { ScheduleComponent } from '../schedule/schedule.component';
import { ActivityAddDialog } from 'src/app/models/activity/add-activity-dialog';
import { TagService } from 'src/app/services/tag.service';
import { TagInfo } from 'src/app/models/tag/tag-info';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'task-add-dialog',
  templateUrl: './task-add-dialog.component.html',
  styleUrls: ['./task-add-dialog.component.css']
})
export class TaskAddDialogComponent implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;
  
  public isTagActive: boolean = false;
  public isRepeatable: boolean = false;

  public userTags = [] as TagInfo[];
  public userId: number;

  colorInput: HTMLInputElement;
  public color = "#FFA500";

  constructor(
    private formBuilder: FormBuilder,
    private tagService: TagService,
    private dialogRef: MatDialogRef<ScheduleComponent>,
    @Inject(MAT_DIALOG_DATA) public data: number) {
      this.colorInput = document.getElementById('colorpicker') as HTMLInputElement;
      this.userId = data;
      this.tagService.getAllTagsInfo(this.userId)
        .subscribe((tags) => {
          this.userTags = tags;
        })
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
        color: (<HTMLInputElement>document.getElementById("colorpicker")).value
      }
      this.dialogRef.close(activity);
    }

    changeTagState() {
      this.isTagActive = !this.isTagActive;
    }

    searchTagColor(name: string) {
      let color = this.userTags.find(tag => tag.name == name)?.color as string;
      if(color == null) { 
        return "#FFA500";
      }
      else {
        return color;
      }
    }

    onKey(event: any) {
      this.color = this.searchTagColor(event.target.value);
    }
}
