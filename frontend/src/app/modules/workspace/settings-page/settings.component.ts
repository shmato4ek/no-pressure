import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { EMPTY, Subject, catchError, takeUntil } from 'rxjs';
import { ActivityDTO } from 'src/app/models/activity/activity-dto';
import { PlanDTO } from 'src/app/models/plan/plan-dto';
import { Statistic } from 'src/app/models/statistic/statistic';
import { Subscriptions } from 'src/app/models/subscriptions/subscriptions';
import { UserSubscription } from 'src/app/models/subscriptions/user-subscription';
import { UserDTO } from 'src/app/models/user/user-dto';
import { ActivityService } from 'src/app/services/activity.service';
import { PlanService } from 'src/app/services/plan.service';
import { RegistrationService } from 'src/app/services/registration.service';
import { UserService } from 'src/app/services/user.service';
import { SubscriptionDialogComponent } from '../subscriptions-dialog/subscription-dialog.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomValidators } from '../../validators/custom-validators';
import { SettingsPrivacy } from 'src/app/models/enums/settings-privacy';
import { Settings } from 'src/app/models/settings/settings';
import { SettingsService } from 'src/app/services/settings.service';
import { UpdateUser } from 'src/app/models/user/update-user';
import { ChangePassword } from 'src/app/models/user/change-password';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { CacheResourceService } from 'src/app/services/cache.resource.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  public currentUser = {} as UserDTO;
  public isAppear = false;

  public passwordForm: FormGroup = {} as FormGroup;
  public settingsForm: FormGroup = {} as FormGroup;
  public updateForm: FormGroup = {} as FormGroup;

  passwordType = '';
  imgSrc = '';
  showPassword = false;

  newPasswordType = '';
  newImgSrc = '';
  showNewPassword = false;

  currentStatChecked = {} as SettingsPrivacy;
  currentActChecked = {} as SettingsPrivacy;

  isValid = false;

  private unsubscribe$ = new Subject<void>();

  constructor(
    private registrationService: RegistrationService,
    public dialog: MatDialog,
    private formBuilder: FormBuilder,
    private settingsService: SettingsService,
    private userService: UserService,
    private snackBarService: SnackBarService,
    private cacheResurceService: CacheResourceService,
  ) {}
  

  async ngOnInit(): Promise<void> {
    this.passwordType = this.newPasswordType = 'password';
    this.imgSrc = this.newImgSrc = '../../../../assets/img/show-password.svg';

    this.validateForm();
    this.validateCheckBox();
    this.validateUpdateForm()
  
    await this.cacheResurceService
      .getUser()
      .then((user) => {
        if (user != undefined) {
          this.currentUser = user;
          this.setUpdateForm();
          this.settingsService.getSettings()
            .subscribe((resp) => {
              this.currentStatChecked = resp.statistic,
              this.currentActChecked = resp.activities
            })
        }
      })

      this.setSettingsForm();
  }

  private validateForm() {
    this.passwordForm = this.formBuilder.group({
      oldPassword: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(20),
        Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$')
      ]],
      newPassword: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(20),
        Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$')
      ]]},
      {
        validator: CustomValidators.changePasswordValidation
      });
  }

  private validateCheckBox() {
    this.settingsForm = this.formBuilder.group({
      checkbox11: ['',],
      checkbox12: ['',],
      checkbox13: ['',],
      checkbox21: ['',],
      checkbox22: ['',],
      checkbox23: ['',],
    })
  }

  private validateUpdateForm() {
    this.updateForm = this.formBuilder.group({
      username: ['', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(15)
      ]],
      email: ['', [
        Validators.required,
        Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
      ]],
    })
  }

  togglePassword(type: string) {
    if (type == 'psw') {
      this.showPassword = !this.showPassword;
      if (this.showPassword)
      {
        this.passwordType = 'text';
        this.imgSrc = '../../../../assets/img/hide-password.svg';
      }
      else
      {
        this.passwordType = 'password';
        this.imgSrc = '../../../../assets/img/show-password.svg';
      }
    }
    else {
      this.showNewPassword = !this.showNewPassword;
      if (this.showNewPassword)
      {
        this.newPasswordType = 'text';
        this.newImgSrc = '../../../../assets/img/hide-password.svg';
      }
      else
      {
        this.newPasswordType = 'password';
        this.newImgSrc = '../../../../assets/img/show-password.svg';
      }
    }
  }

  setSettingsForm() {
    this.settingsForm.get('checkbox11')?.setValue(this.currentStatChecked == 0);
    this.settingsForm.get('checkbox12')?.setValue(this.currentStatChecked == 1);
    this.settingsForm.get('checkbox13')?.setValue(this.currentStatChecked == 2);

    this.settingsForm.get('checkbox21')?.setValue(this.currentActChecked == 0);
    this.settingsForm.get('checkbox22')?.setValue(this.currentActChecked == 1);
    this.settingsForm.get('checkbox23')?.setValue(this.currentActChecked == 2);
  }

  setUpdateForm() {
    this.updateForm.get('username')?.setValue(this.currentUser.name);
    this.updateForm.get('email')?.setValue(this.currentUser.email);
    this.updateForm.get('country')?.setValue('Ukraine');
  }

  selectStatCheckBox(checked: SettingsPrivacy) {
    this.currentStatChecked = checked;
    this.setSettingsForm();
  }

  selectActCheckBox(checked: SettingsPrivacy) {
    this.currentActChecked = checked;
    this.setSettingsForm();
  }

  openSnackBar(target: string) {
    this.snackBarService.openSnackBar(`${target} must contain only latin symbols!`);
  }

  snackBarPasswordChanged() {
    this.snackBarService.openSnackBar('Password was successfully changed!');
  }

  snackBarPasswordError() {
    this.snackBarService.openSnackBar('Old password is invalid!');
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

  changePassword() {
    var password = this.passwordForm.get('oldPassword')?.value;
    var encodedPassword = btoa(password);
    this.userService
      .checkPassword(encodedPassword)
      .subscribe((resp) => {
        this.isValid = resp;

        if(this.isValid) {
          let changePassword: ChangePassword = {
            userId: this.currentUser.id,
            oldPassword: password,
            newPassword: this.passwordForm.get('newPassword')?.value,
          }
    
          this.userService.changePassword(changePassword).subscribe();
          this.snackBarPasswordChanged();
        } else {
          this.snackBarPasswordError();
        }
      });
  }

  changeStat() {
    const updatedSettings: Settings = {
      statistic: this.currentStatChecked,
      activities: this.currentActChecked,
    }

    this.settingsService.updateSettings(updatedSettings);
    this.snackBarService.openSnackBar("Settings were successfully updated!");
  }

  updateUser() {
    let user: UpdateUser = {
      id: this.currentUser.id,
      name: this.updateForm.get('username')?.value,
      email: this.updateForm.get('email')?.value,
    }

    if(user.email == this.currentUser.email && user.name == this.currentUser.name) {
      this.snackBarService.openSnackBar('There was no changes in username or email');
      return;
    }

    this.userService.updateUser(user).subscribe();
    this.snackBarService.openSnackBar("Settings were successfully updated!");
  }
}
