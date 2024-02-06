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
import { Router } from '@angular/router';
import { TeamComponent } from '../team/team.component';

@Component({
  selector: 'team-settings-dialog',
  templateUrl: './team-settings-dialog.component.html',
  styleUrls: ['./team-settings-dialog.component.css']
})
export class TeamSettingDialog implements OnInit{
  dialogForm: FormGroup = {} as FormGroup;

  teamId: number;
  color: string;
  name: string;
  team = {} as TeamWithSettings;
  ownerIndex = {} as number;

  currentPrivacyChecked = {} as TeamPrivacyState;

  colorInput: HTMLInputElement;

  constructor(
    private snackBarService: SnackBarService,
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<TeamComponent>,
    private teamService: TeamService,
    private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: SettingsTeamInfo) {
      this.colorInput = document.getElementById('colorpicker') as HTMLInputElement;
      this.teamId = data.teamId;
      this.color = data.color;
      this.name = data.teamName;
      this.teamService
        .getTeamWithSettings(this.teamId)
        .subscribe((resp) => {
          this.team = resp;
          this.buildSettingsForm();
          this.currentPrivacyChecked = this.team.state;
          this.setSettingsForm();
          this.ownerIndex = this.setOwnerIndex(data.ownerId);
        })
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
          teamState1: [this.team.state == TeamPrivacyState.Private],
          teamState2: [this.team.state == TeamPrivacyState.Public],
          settings: this.formBuilder.array([]),
        });

        this.dialogForm.get('teamName')?.setValue(this.name);
    }

    get settings() {
      return <FormArray>this.dialogForm.controls['settings'];
    }

    buildSettingsForm () {
      this.team.settings.forEach(element => {
        const settingsForm = this.formBuilder.group({
          id: [element.id],
          users: [element.addingUsers == TeamAccess.Allow],
          activities: [element.addingActivities == TeamAccess.Allow],
        });
        this.settings.push(settingsForm);
      });
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

    setOwnerIndex(id: number) {
      const index = this.team.settings.findIndex(t => t.userId == id);
      return index;
    }

    setSettingsForm() {
      this.dialogForm.get('teamState1')?.setValue(this.currentPrivacyChecked == TeamPrivacyState.Private);
      this.dialogForm.get('teamState2')?.setValue(this.currentPrivacyChecked == TeamPrivacyState.Public);
    }

    selectStateCheckBox(checked: TeamPrivacyState) {
      this.currentPrivacyChecked = checked;
      this.setSettingsForm();
    }

    save() {
      let updateSettings: UpdateSettings[] = [];

      for (let s of this.settings.value) {
        let userSettings: UpdateSettings = {
          id: s.id,
          addingUsers: s.users? TeamAccess.Allow : TeamAccess.Deny,
          addingActivities: s.activities? TeamAccess.Allow : TeamAccess.Deny,
        }

        updateSettings.push(userSettings);
      };

      let updateTeamSettings: UpdateTeamSettings = {
        teamId: this.team.id,
        state: this.currentPrivacyChecked,
        color: (<HTMLInputElement>document.getElementById("colorpicker")).value,
        settings: updateSettings,
      }

      this.dialogRef.close(updateTeamSettings);
    }

    delete() {
      if(confirm("Are you sure to delete team?")) {
        this.teamService.deleteTeam(this.team.id);
        this.router.navigate(['/teams'])
        this.dialogRef.close();
      }
    }
}
