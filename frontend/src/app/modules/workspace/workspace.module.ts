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
import { TaskScheduleDialogComponent } from './task-schedule-dialog/task-schedule-dialog-component';
import { ScheduledTaskDialogComponent } from './scheduled-task-dialog/scheduled-task-dialog.component';
import { TagEditDialogComponent } from './tag-edit-dialog/tag-edit-dialog.component';
import { ProfileComponent } from './profile/profile.component';
import { PlanEditDialogComponent } from './plan-edit-dialog/plan-edit-dialog.component';
import { ConvertToGoalDialog } from './convert-to-goal-dialog/convert-to-goal-dialog.component';
import { GoalsComponent } from './goals/goals.component';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { SubscriptionDialogComponent } from './subscriptions-dialog/subscription-dialog.component';
import { SharedProfileComponent } from './shared-profile/shared-profile.component';
import { SettingsComponent } from './settings-page/settings.component';
import { NotificationsDialogComponent } from './notifications-dialog/notifications-dialog.component';
import { TeamsComponent } from './teams-page/teams.component';
import { TeamComponent } from './team/team.component';
import { AddTeamDilogComponent } from './add-team-dialog/add-team-dialog.component';
import { TeamSettingDialog } from './team-settings-dialog/team-settings-dialog.component';
import { AddUsersToTeamDialog } from './add-users-to-team-dialog/add-users-to-team-dialog.component';
import { DropdownModule } from 'primeng/dropdown';

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
        ConvertToGoalDialog,
        GoalsComponent,
        SubscriptionDialogComponent,
        SharedProfileComponent,
        SettingsComponent,
        NotificationsDialogComponent,
        TeamsComponent,
        TeamComponent,
        AddTeamDilogComponent,
        TeamSettingDialog,
        AddUsersToTeamDialog,
    ],
    imports: [
        WorkspaceRoutingModule,
        MaterialModule,
        CommonModule,
        ReactiveFormsModule,
        DropdownModule
    ],
})

export class WorkspaceModule {}