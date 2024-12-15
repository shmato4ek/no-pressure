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
import { ScheduleHour } from 'src/app/models/enums/ScheduleHour';

@Component({
  selector: 'task-add-dialog',
  templateUrl: './task-add-dialog.component.html',
  styleUrls: ['./task-add-dialog.component.css']
})
export class TaskAddDialogComponent implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;
  
  public isTagActive: boolean = false;
  public isRepeatable: boolean = false;

  PRIORITY_SLIDER_MAX = 5;

  public userTags = [] as TagInfo[];
  public userTagsString = [] as string[];
  directiveTermOptions: number[];
  durationOptions: number[];
  public userId: number;
  public teamId: number;

  _repeatable_tooltip = "If the activity is repeatable, you can add it to the schedule again on another day";

  colorInput: HTMLInputElement;
  public color = "#FFA500";
  
  public selectedTag = "";

  constructor(
    private snackBarService: SnackBarService,
    private formBuilder: FormBuilder,
    private tagService: TagService,
    private dialogRef: MatDialogRef<ScheduleComponent>,
    @Inject(MAT_DIALOG_DATA) public data: NewActivityInfo) {
      this.colorInput = document.getElementById('colorpicker') as HTMLInputElement;
      this.userId = data.userId;
      this.teamId = data.teamId;
      this.directiveTermOptions = [7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23];
      this.durationOptions = [1, 2, 3, 4, 5, 6, 7, 8];
      if (this.teamId == 0) {
      this.tagService.getAllTagsInfo(this.userId)
        .subscribe((tags) => {
          this.userTags = tags;
          tags.forEach(element => {
            this.userTagsString.push(element.name);
          });
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
              Validators.maxLength(15),
            ],
            updateOn:'change',
          }],
          activityDescription: [,{
            validators: [
              Validators.maxLength(15),
            ],
            updateOn:'change',
          }],
          activityTag: [,{
            validators: [
              Validators.maxLength(10)
            ]
          }],
          activityPriority: [,{
            validators: []
          }],
          activityDirectiveTerm: [,{
            validators: []
          }],
          activityDelayCoefficient: [,{
            validatirs: []
          }],
          activityDuration: [,{
            validators: []
          }]
        });
    }

    save() {
      let directiveTerm = this.dialogForm.get('activityDirectiveTerm')?.value as ScheduleHour;
      let activity: ActivityAddDialog = {
        name: this.dialogForm.value.activityName,
        description: this.dialogForm.value.activityDescription,
        isRepeatable: this.isRepeatable,
        color: this.color,
        teamId: this.teamId,
        priority: this.dialogForm.value.activityPriority,
        directiveTerm: directiveTerm,
        delayCoefficient: this.dialogForm.value.activityDelayCoefficient,
        duration: this.dialogForm.value.activityDuration
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
        (<HTMLInputElement>document.getElementById("colorpicker")).disabled = true;
        return color;
      }
    }

    onKey(event: any) {
      this.color = this.searchTagColor(event.target.value);
    }

    inputValidation(event: any, target: string) {   
      var k;  
      k = event.charCode;
      var isValid = (
        (k > 64 && k < 91) || 
        (k > 96 && k < 123) ||
        k == 8 ||
        k == 32 ||
        (k >= 48 && k <= 57) ||
        (k >= 33 && k <= 47) ||
        (k >= 58 && k <= 64) ||
        (k >= 91 && k <= 96) ||
        (k >= 123 && k <= 126));
      if (!isValid) {
        this.openSnackBar(target);
      }
      return isValid; 
    }

    openSnackBar(target: string) {
      this.snackBarService.openSnackBar(`${target} must contain only latin symbols!`);
    }

    close() {
      this.dialogRef.close();
    }
}
