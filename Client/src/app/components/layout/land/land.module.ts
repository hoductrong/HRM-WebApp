import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LandRoutingModule } from './land-routing.module';
import { LandComponent } from './land.component';

@NgModule({
  imports: [
    CommonModule,
    LandRoutingModule
  ],
  declarations: [LandComponent]
})
export class LandModule { }
