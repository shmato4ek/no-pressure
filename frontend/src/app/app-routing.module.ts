import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TokenExistsGuard } from './guards/token-exists.guard';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => 
      import('./modules/workspace/workspace.module').then(
        (m) => m.WorkSpaceModule
      ),
      //canActivate: [TokenExistsGuard],
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./modules/auth/auth.module').then(
        (m) => m.AuthModule
      )
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
