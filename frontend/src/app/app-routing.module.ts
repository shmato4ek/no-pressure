import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TokenExistsGuard } from './guards/token-exists.guard';
import { WorkspaceBaseComponent } from './modules/workspace/workspace-base/w-base.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => 
      import('./modules/home/home.module').then(
        (m) => m.HomeModule
      ),
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./modules/auth/auth.module').then(
        (m) => m.AuthModule
      )
  },
  {
    path: 'personal',
    loadChildren: () =>
      import('./modules/workspace/workspace.module').then(
        (m) => m.WorkspaceModule
      ),
    canActivate: [TokenExistsGuard],
  },
  {
    path: 'profile/:id',
    component: WorkspaceBaseComponent,
    canActivate: [TokenExistsGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
