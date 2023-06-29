import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { WorkspaceBaseComponent } from "./workspace-base/w-base.component";

const routes: Routes = [
    {
        path: '',
        component: WorkspaceBaseComponent,
        children: [
        ]
    }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class WorkspaceRoutingModule {}