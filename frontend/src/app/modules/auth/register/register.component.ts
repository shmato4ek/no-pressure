import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UserRegister } from 'src/app/models/user/user-register';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
  public registerForm: FormGroup = {} as FormGroup;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
  ) {}

  ngOnInit() {
      this.validateForm();
  }

  private validateForm() {
    this.registerForm = this.formBuilder.group(
      {
        username: [
          ,
          [
            Validators.required,
            Validators.pattern(
              '^[а-яА-ЯёЁa-zA-Z\`\'][а-яА-ЯёЁa-zA-Z-\`\' ]+[а-яА-ЯёЁa-zA-Z\`\']?$'
            ),
            Validators.minLength(3),
            Validators.maxLength(15),
          ],
        ],
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

  public registerUser() {
    let userRegistered: UserRegister = {
      email: this.registerForm.controls['email'].value,
      username: this.registerForm.controls['username'].value,
      password:this.registerForm.controls['password'].value
    }

    console.log(userRegistered);
  }
}
