import { NgModule } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { WorkspaceRoutingModule } from './workspace-routing.module';

import { WorkspaceBaseComponent } from './workspace-base/w-base.component';
import { WorkspaceHeaderComponent } from './workspace-header/w-header.component';
import { UserNavbarComponent } from './user-navbar/u-navbar.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/services/interceptor';

@NgModule({
    declarations: [
        WorkspaceBaseComponent,
        WorkspaceHeaderComponent,
        UserNavbarComponent,
        ScheduleComponent
    ],
    imports: [
        WorkspaceRoutingModule,
        MaterialModule
    ],
    providers: [

    ]
})

export class WorkspaceModule {}