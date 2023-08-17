import { NgModule } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { WorkspaceRoutingModule } from './workspace-routing.module';

import { WorkspaceBaseComponent } from './workspace-base/w-base.component';
import { WorkspaceHeaderComponent } from './workspace-header/w-header.component';
import { UserNavbarComponent } from './user-navbar/u-navbar.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { CommonModule } from '@angular/common';

@NgModule({
    declarations: [
        WorkspaceBaseComponent,
        WorkspaceHeaderComponent,
        UserNavbarComponent,
        ScheduleComponent
    ],
    imports: [
        WorkspaceRoutingModule,
        MaterialModule,
        CommonModule
    ],
    providers: [

    ]
})

export class WorkspaceModule {}