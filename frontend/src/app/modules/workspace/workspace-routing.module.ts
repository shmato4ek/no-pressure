import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { WorkspaceBaseComponent } from "./workspace-base/w-base.component";
import { ScheduleComponent } from "./schedule/schedule.component";
import { PlansComponent } from "./plans/plans.component";
import { ProfileComponent } from "./profile/profile.component";
import { GoalsComponent } from "./goals/goals.component";

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
            }
        ]
    }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class WorkspaceRoutingModule {}