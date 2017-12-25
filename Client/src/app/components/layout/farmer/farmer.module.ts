import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FarmerRoutingModule } from './farmer-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FarmerComponent } from './farmer.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap/modal/modal';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgbModule.forRoot(),
    FarmerRoutingModule
  ],
  declarations: [FarmerComponent]
})
export class FarmerModule { }
