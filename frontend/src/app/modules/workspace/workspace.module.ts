import { NgModule } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { WorkspaceRoutingModule } from './workspace-routing.module';

import { WorkspaceBaseComponent } from './workspace-base/w-base.component';
import { WorkspaceHeaderComponent } from './workspace-header/w-header.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { TaskAddDialogComponent } from './task-adding-dialog/task-add-dialog.component';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TaskDialogComponent } from './task-dialog/task-dialog.component';
import { PlansComponent } from './plans/plans.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { TaskScheduleDialogComponent } from './task-schedule-dialog.ts/task-schedule-dialog-component';
import { ScheduledTaskDialogComponent } from './scheduled-task-dialog/scheduled-task-dialog.component';
import { TagEditDialogComponent } from './tag-edit-dialog/tag-edit-dialog.component';
import { ProfileComponent } from './profile/profile.component';
import { PlanEditDialogComponent } from './plan-edit-dialog/plan-edit-dialog.component';
import { ConvertToGoalDialog } from './convert-to-goal-dialog/convert-to-goal-dialog.component';

@NgModule({
    declarations: [
        WorkspaceBaseComponent,
        WorkspaceHeaderComponent,
        ScheduleComponent,
        TaskAddDialogComponent,
        TaskDialogComponent,
        TaskScheduleDialogComponent,
        PlansComponent,
        SidenavComponent,
        ScheduledTaskDialogComponent,
        TagEditDialogComponent,
        ProfileComponent,
        PlanEditDialogComponent,
        ConvertToGoalDialog
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