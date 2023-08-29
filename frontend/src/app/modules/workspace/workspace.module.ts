import { NgModule } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { WorkspaceRoutingModule } from './workspace-routing.module';

import { WorkspaceBaseComponent } from './workspace-base/w-base.component';
import { WorkspaceHeaderComponent } from './workspace-header/w-header.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { TaskAddDialogComponent } from './task-adding-dialog/task-add-dialog.component';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TaskUpdateDialogComponent } from './task-update-dialog/task-update-dialog.component';
import { PlansComponent } from './plans/plans.component';
import { SidenavComponent } from './sidenav/sidenav.component';

@NgModule({
    declarations: [
        WorkspaceBaseComponent,
        WorkspaceHeaderComponent,
        ScheduleComponent,
        TaskAddDialogComponent,
        TaskUpdateDialogComponent,
        PlansComponent,
        SidenavComponent,
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