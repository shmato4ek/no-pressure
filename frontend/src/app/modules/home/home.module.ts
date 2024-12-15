import { NgModule } from '@angular/core';
import { BaseComponent } from './base/base.component';
import { HeaderComponent } from './header/header.component';
import { HomeRoutingModule } from './home-routing.module';
import { MaterialModule } from '../material/material.module';
import { HomeComponent } from './home/home.component';

@NgModule({
    declarations: [
        BaseComponent,
        HeaderComponent,
        HomeComponent
    ],
    imports: [
        HomeRoutingModule,
        MaterialModule
    ],
    providers: []
})

export class HomeModule {}