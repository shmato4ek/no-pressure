import { NgModule } from '@angular/core';
import { BaseComponent } from './base/base.component';
import { HeaderComponent } from './header/header.component';
import { BaseRoutingModule } from './workspace-routing.module';
import { MaterialModule } from '../material/material.module';
import { HomeComponent } from './home/home.component';

@NgModule({
    declarations: [
        BaseComponent,
        HeaderComponent,
        HomeComponent
    ],
    imports: [
        BaseRoutingModule,
        MaterialModule
    ],
    providers: []
})

export class WorkSpaceModule {}