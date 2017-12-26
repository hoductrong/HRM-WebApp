import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './components/shared';

const routes : Routes = [
  { path: '', loadChildren: './components/layout/layout.module#LayoutModule', canActivate: [AuthGuard] },
  { path: 'login', loadChildren: './components/login/login.module#LoginModule' },
  { path: 'error', loadChildren: './components/server-error/server-error.module#ServerErrorModule' },
  { path: 'access-denied', loadChildren: './components/access-denied/access-denied.module#AccessDeniedModule' },
  { path: 'not-found', loadChildren: './components/not-found/not-found.module#NotFoundModule' },
  { path: '**', redirectTo: 'not-found' }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [ RouterModule ],
  declarations: []
})
export class AppRoutingModule { }
