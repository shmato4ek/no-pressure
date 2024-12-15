import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
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
import { Settings } from 'src/app/models/settings/settings';
import { SettingsTeamInfo } from 'src/app/models/team/settings-team-info';
import { TeamService } from 'src/app/services/team.service';
import { TeamWithSettings } from 'src/app/models/team/team-with-settings';
import { TeamPrivacyState } from 'src/app/models/enums/TeamPrivacyState';
import { SettingsCheckbox } from 'src/app/models/team/settings-checkbox';
import { TeamAccess } from 'src/app/models/enums/TeamAccess';
import { UpdateTeamSettings } from 'src/app/models/team/update-team-settings';
import { UpdateSettings } from 'src/app/models/team/update-settings';
import { TeamComponent } from '../team/team.component';
import { AddUsersToTeam } from 'src/app/models/team/add-users-to-team';

@Component({
  selector: 'add-users-to-team-dialog',
  templateUrl: './add-users-to-team-dialog.component.html',
  styleUrls: ['./add-users-to-team-dialog.component.css']
})
export class AddUsersToTeamDialog implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;

  teamId: number;

  constructor(
    private snackBarService: SnackBarService,
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<TeamComponent>,
    private teamService: TeamService,
    @Inject(MAT_DIALOG_DATA) public data: number) {
      this.teamId = data;
    }

    ngOnInit(): void {
      this.dialogForm = this.formBuilder.group({
        user: [,{
          validators: [
            Validators.required,
            Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')
          ],
          updateOn:'change',
        }],
        users: this.formBuilder.array([]),
      });
    }

    get users() {
      return <FormArray>this.dialogForm.controls['users'];
    }

    addUser() {
      const userForm = this.formBuilder.group({
          name: [,{
            validators: [
              Validators.required,
              Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')
            ],
            updateOn:'change',
          }],
      });
      this.users.push(userForm);
    }

    deleteUser(userIndex: number) {
      this.users.removeAt(userIndex);
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

    save() {
      let newUsers: string[] = [
        this.dialogForm.value.user
      ];

      for(let user of this.users.value) {
        newUsers.push(user.name);
      }

      let addUsers: AddUsersToTeam = {
        teamId: this.teamId,
        users: newUsers,
      }

      this.dialogRef.close(addUsers);
    }

    close() {
      this.dialogRef.close();
    }
}
