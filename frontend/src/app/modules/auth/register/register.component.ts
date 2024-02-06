import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControlOptions } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UserDTO } from 'src/app/models/user/user-dto';
import { UserRegister } from 'src/app/models/user/user-register';
import { LoginService } from 'src/app/services/login.service';
import { RegistrationService } from 'src/app/services/registration.service';
import { CustomValidators } from '../../validators/custom-validators';
import { SnackBarService } from 'src/app/services/snack-bar.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
  public registerForm: FormGroup = {} as FormGroup;
  public currentUser: UserDTO = {} as UserDTO;
  redirectUrl: string | undefined;

  passwordType = '';
  imgSrc = '';
  showPassword = false;

  confirmPasswordType = '';
  confImgSrc = '';
  showConfirmPassword = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private registrationService: RegistrationService,
    private loginService: LoginService,
    private snackBarService: SnackBarService,
  ) {}

  ngOnInit() {
    this.passwordType = this.confirmPasswordType = 'password';
    this.imgSrc = this.confImgSrc = '../../../../assets/img/show-password.svg';
    this.validateForm();
    this.route.queryParams.subscribe((params) => {
      this.redirectUrl = params['redirect_url'];
    });
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
      this.showConfirmPassword = !this.showConfirmPassword;
      if (this.showConfirmPassword)
      {
        this.confirmPasswordType = 'text';
        this.confImgSrc = '../../../../assets/img/hide-password.svg';
      }
      else
      {
        this.confirmPasswordType = 'password';
        this.confImgSrc = '../../../../assets/img/show-password.svg';
      }
    }
  }

  private validateForm() {
    this.registerForm = this.formBuilder.group({
      username: ['', [
        Validators.required,
        Validators.pattern('(?![_.])[a-zA-Z0-9._]+(?<![_.])$'),
        Validators.minLength(3),
        Validators.maxLength(15)
      ]],
      email: ['', [
        Validators.required,
        Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
      ]],
      password: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(20),
        Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$')
      ]],
      confirmPassword: ['', Validators.required]},
      {
        validator: CustomValidators.passwordMatch
      });
  }

  public registerUser() {
    let userRegistered: UserRegister = {
      email: this.registerForm.controls['email'].value,
      name: this.registerForm.controls['username'].value,
      password:this.registerForm.controls['password'].value
    }

    this.registrationService.register(userRegistered).subscribe({
      next: (responce) => {
        this.currentUser = responce;
        if (this.loginService.areTokensExist()) {
          this.router.navigate(['/personal/schedule'])
        }
      },
    })
  }

  public emailCheck() {
    const email = this.registerForm.get('email')?.value;
    this.registrationService.emailCheck(email).subscribe((resp) => {
      if (!resp?.availability) {
        this.registerForm.get('email')?.setErrors({emailNotAvailable: true})
      }
    })
  }

  public redirectToHome() {
    this.router.navigate(['']);
  }

  public switchToLogin() {
    this.router.navigate(['login']);
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