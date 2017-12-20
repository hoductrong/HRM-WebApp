import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FarmerRoutingModule } from './farmer-routing.module';
import { FarmerComponent } from './farmer.component';

@NgModule({
  imports: [
    CommonModule,
    FarmerRoutingModule
  ],
  declarations: [FarmerComponent]
})
export class FarmerModule { }
