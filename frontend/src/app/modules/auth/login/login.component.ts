import { GoogleLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component, ElementRef, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { take } from 'rxjs';
import { ExternalUserAuth } from 'src/app/models/user/external-auth-user';
import { UserDTO } from 'src/app/models/user/user-dto';
import { UserLogin } from 'src/app/models/user/user-login';
import { LoginService } from 'src/app/services/login.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup = {} as FormGroup;
  public currentUser: UserDTO = {} as UserDTO;
  redirectUrl: string | undefined;
  googleUser = {} as SocialUser;
  googleBtn?: ElementRef;

  passwordType = '';
  imgSrc = '';
  showPassword = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private loginService: LoginService,
    private googleAuthService: SocialAuthService,
    private userService: UserService,
  ) {}

  ngOnInit() {
    this.passwordType = 'password';
    this.imgSrc = '../../../../assets/img/show-password.svg';
      this.validateForm();
      this.route.queryParams.subscribe((params) => {
        this.redirectUrl = params['redirect_url'];
      });

      this.googleAuthService.authState.subscribe((resp:SocialUser) => {
        this.googleUser = resp;
        if(this.googleUser) {
          let user: ExternalUserAuth = {
            name: resp.name,
            email: resp.email,
            authToken: resp.id
          };
          this.loginService.googleAuth(user).subscribe({
            next: (responce) => {   
              this.currentUser = responce;
              if (this.loginService.areTokensExist()) {
                this.router.navigate(['/personal/schedule'])
              }
            },
          })
        }
      })
  }

  private validateForm() {
    this.loginForm = this.formBuilder.group(
      {
        email: [
          ,
          [
            Validators.required,
            Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
          ],
        ],
        password: [
          ,
          [
            Validators.required,
            Validators.minLength(8),
            Validators.maxLength(20),
            Validators.pattern('^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$'),
          ],
        ],
      },
    );
  }

  public loginUser() {
    let userLogined: UserLogin = {
      email: this.loginForm.controls['email'].value,
      password:this.loginForm.controls['password'].value
    }
    this.loginService.login(userLogined).subscribe({
      next: (responce) => {   
        this.currentUser = responce;
        if (this.loginService.areTokensExist()) {
          this.router.navigate(['/personal/schedule'])
        }
      },
    })
  }

  togglePassword() {
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

  public redirectToHome() {
    this.router.navigate(['']);
  }

  public switchToRegister() {
    this.router.navigate(['auth']);
  }
}
