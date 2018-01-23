import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoleManagerRoutingModule } from './role-manager-routing.module';
import { RoleManagerComponent } from './role-manager.component';
import { RoleService } from './../../shared/services/role.service';
import { FilterPipeModule } from 'ngx-filter-pipe';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RoleManagerRoutingModule,
    FilterPipeModule
  ],
  declarations: [RoleManagerComponent],
  providers:[RoleService]
})
export class RoleManagerModule { }
