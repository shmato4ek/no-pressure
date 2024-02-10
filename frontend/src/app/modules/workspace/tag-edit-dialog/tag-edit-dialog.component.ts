import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogConfig, MatDialogRef, MatDialog } from '@angular/material/dialog';
import { ScheduleComponent } from '../schedule/schedule.component';
import { ActivityAddDialog } from 'src/app/models/activity/add-activity-dialog';
import { UpdateActivity } from 'src/app/models/activity/update-activity';
import { ActivityService } from 'src/app/services/activity.service';
import { TaskScheduleDialogComponent } from '../task-schedule-dialog/task-schedule-dialog-component';
import { ScheduleService } from 'src/app/services/schedule.service';
import { AddTaskToSchedule } from 'src/app/models/schedule/add-task-to-schedule';
import { UpdateActivityDialog } from 'src/app/models/activity/update-activity-dialog';
import { ActivityDTO } from 'src/app/models/activity/activity-dto';
import { Tag } from 'src/app/models/tag/tag';
import { UpdateTag } from 'src/app/models/tag/update-tag';
import { SnackBarService } from 'src/app/services/snack-bar.service';

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
    private snackBarService: SnackBarService,
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
              Validators.maxLength(10),
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

    close() {
      this.dialogRef.close();
    }
}
