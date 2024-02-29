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
import { SnackBarService } from 'src/app/services/snack-bar.service';

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
    private snackBarService: SnackBarService,
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ScheduleComponent>,
    private activityService: ActivityService,
    private scheduleService: ScheduleService,
    public dialog: MatDialog,
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
              Validators.maxLength(15),
            ],
            updateOn:'change',
          }],
          activityDescription: [,{
            validators: [
              Validators.maxLength(15),
            ],
            updateOn:'change',
          }]
        });
        this.dialogForm.get('activityName')?.setValue(this.activityName);
        this.dialogForm.get('activityDescription')?.setValue(this.activityDescription);
    }

    save() {
      let activity: UpdateActivityDialog = {
        name: this.dialogForm.value.activityName,
        description: this.dialogForm.value.activityDescription,
      }
      this.dialogRef.close(activity);
    }

    delete() {
      this.activityService.delete(this.activityId).subscribe();
      window.location.reload();
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
