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
import { NewTeam } from 'src/app/models/team/new-team';

@Component({
  selector: 'team-add-dialog',
  templateUrl: './add-team-dialog.component.html',
  styleUrls: ['./add-team-dialog.component.css']
})
export class AddTeamDilogComponent implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;

  public userId: number;

  colorInput: HTMLInputElement;
  public color = "#FFA500";

  constructor(
    private snackBarService: SnackBarService,
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<ScheduleComponent>,
    @Inject(MAT_DIALOG_DATA) public data: number) {
      this.colorInput = document.getElementById('colorpicker') as HTMLInputElement;
      this.userId = data;
    }

    ngOnInit(): void {
        this.dialogForm = this.formBuilder.group({
          teamName: [,{
            validators: [
              Validators.required,
              Validators.maxLength(30),
            ],
            updateOn:'change',
          }],
        });
    }

    save() {
      let team: NewTeam = {
        name: this.dialogForm.value.teamName,
        color: (<HTMLInputElement>document.getElementById("colorpicker")).value,
        userId: this.userId,
      }
      this.dialogRef.close(team);
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
