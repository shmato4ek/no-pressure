import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { WorkspaceBaseComponent } from "./workspace-base/w-base.component";
import { ScheduleComponent } from "./schedule/schedule.component";
import { PlansComponent } from "./plans/plans.component";
import { ProfileComponent } from "./profile/profile.component";

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
            }
        ]
    }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class WorkspaceRoutingModule {}