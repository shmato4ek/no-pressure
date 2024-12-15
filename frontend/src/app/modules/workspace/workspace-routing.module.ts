import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { WorkspaceBaseComponent } from "./workspace-base/w-base.component";
import { ScheduleComponent } from "./schedule/schedule.component";
import { PlansComponent } from "./plans/plans.component";
import { ProfileComponent } from "./profile/profile.component";
import { GoalsComponent } from "./goals/goals.component";
import { SettingsComponent } from "./settings-page/settings.component";
import { TeamsComponent } from "./teams-page/teams.component";
import { TeamComponent } from "./team/team.component";
import { ErrorPageComponent } from "./error-page/error-page.component";

const routes: Routes = [
    {
        path: '',
        component: WorkspaceBaseComponent,
        children: [
            {
                path: 'schedule',
                component: ScheduleComponent,
            },
            {
                path: 'plans',
                component: PlansComponent,
            },
            {
                path: 'profile',
                component: ProfileComponent,
            },
            {
                path: 'goals',
                component: GoalsComponent,
            },
            {
                path: 'settings',
                component: SettingsComponent,
            },
            {
                path: 'teams',
                component: TeamsComponent,
            },
            {
                path: 'teams/:id',
                component: TeamComponent,
            },
            {
                path: 'error/:status',
                component: ErrorPageComponent
            },
            {
                path: '**',
                pathMatch: 'full',
                component: ErrorPageComponent
            }
        ]
    }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class WorkspaceRoutingModule {}