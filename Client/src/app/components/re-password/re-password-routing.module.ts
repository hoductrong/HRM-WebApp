import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RePasswordComponent } from './re-password.component'

const routes: Routes = [
  {
    path: '',
        component: RePasswordComponent
      }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RePasswordRoutingModule { }
