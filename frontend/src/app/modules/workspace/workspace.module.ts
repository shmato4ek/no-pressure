import { NgModule } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { WorkspaceRoutingModule } from './workspace-routing.module';

import { WorkspaceBaseComponent } from './workspace-base/w-base.component';
import { WorkspaceHeaderComponent } from './workspace-header/w-header.component';
import { UserNavbarComponent } from './user-navbar/u-navbar.component';

@NgModule({
    declarations: [
        WorkspaceBaseComponent,
        WorkspaceHeaderComponent,
        UserNavbarComponent
    ],
    imports: [
        WorkspaceRoutingModule,
        MaterialModule
    ],
    providers: []
})

export class WorkspaceModule {}