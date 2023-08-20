import { NgModule } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { WorkspaceRoutingModule } from './workspace-routing.module';

import { WorkspaceBaseComponent } from './workspace-base/w-base.component';
import { WorkspaceHeaderComponent } from './workspace-header/w-header.component';
import { UserNavbarComponent } from './user-navbar/u-navbar.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { TaskAddDialogComponent } from './task-adding-dialog/task-add-dialog.component';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TaskUpdateDialogComponent } from './task-update-dialog/task-update-dialog.component';

@NgModule({
    declarations: [
        WorkspaceBaseComponent,
        WorkspaceHeaderComponent,
        UserNavbarComponent,
        ScheduleComponent,
        TaskAddDialogComponent,
        TaskUpdateDialogComponent
    ],
    imports: [
        WorkspaceRoutingModule,
        MaterialModule,
        CommonModule,
        ReactiveFormsModule
    ],
    providers: [

    ]
})

export class WorkspaceModule {}