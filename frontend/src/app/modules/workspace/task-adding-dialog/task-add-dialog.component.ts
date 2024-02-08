import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NewActivity } from 'src/app/models/activity/new-activity';
import { ScheduleComponent } from '../schedule/schedule.component';
import { ActivityAddDialog } from 'src/app/models/activity/add-activity-dialog';
import { TagService } from 'src/app/services/tag.service';
import { TagInfo } from 'src/app/models/tag/tag-info';
import { RegistrationService } from 'src/app/services/registration.service';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { NewActivityInfo } from 'src/app/models/activity/new-activity-info';

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
  public teamId: number;

  colorInput: HTMLInputElement;
  public color = "#FFA500";

  constructor(
    private snackBarService: SnackBarService,
    private formBuilder: FormBuilder,
    private tagService: TagService,
    private dialogRef: MatDialogRef<ScheduleComponent>,
    @Inject(MAT_DIALOG_DATA) public data: NewActivityInfo) {
      this.colorInput = document.getElementById('colorpicker') as HTMLInputElement;
      this.userId = data.userId;
      this.teamId = data.teamId;
      if (this.teamId == 0) {
      this.tagService.getAllTagsInfo(this.userId)
        .subscribe((tags) => {
          this.userTags = tags;
        })
      } else {
        this.tagService.getAllTeamTagsInfo(this.teamId)
          .subscribe((tags) => {
            this.userTags = tags;
          })
      }
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
        isRepeatable: this.isRepeatable,
        color: this.color,
        teamId: this.teamId,
      }

      if (this.isTagActive) {
        activity.tag = this.dialogForm.value.activityTag;
        activity.color = (<HTMLInputElement>document.getElementById("colorpicker")).value;
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

    inputValidation(event: any, target: string) {   
      var k;  
      k = event.charCode;
      var isValid = ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
      if (!isValid) {
        this.openSnackBar(target);
      }
      return isValid; 
    }

    openSnackBar(target: string) {
      this.snackBarService.openSnackBar(`${target} must contain only latin symbols!`);
    }
}
