import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UserDTO } from 'src/app/models/user/user-dto';
import { UserLogin } from 'src/app/models/user/user-login';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  public loginForm: FormGroup = {} as FormGroup;
  public currentUser: UserDTO = {} as UserDTO;
  redirectUrl: string | undefined;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private loginService: LoginService,
  ) {}

  ngOnInit() {
      this.validateForm();
      this.route.queryParams.subscribe((params) => {
        this.redirectUrl = params['redirect_url'];
      });
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
        console.log(responce);
      },
    })
  }

  public redirectToHome() {
    this.router.navigate(['']);
  }

  public switchToRegister() {
    this.router.navigate(['auth']);
  }
}
