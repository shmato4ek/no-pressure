import { NgModule } from "@angular/core";
import { MaterialModule } from "../material/material.module";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { AuthRoutinModule } from "./auth-routing-module";
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
    declarations:[
        LoginComponent,
        RegisterComponent
    ],
    imports:[
        MaterialModule,
        AuthRoutinModule,
        ReactiveFormsModule,
    ],
})

export class AuthModule {}