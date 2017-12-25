import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { ImexportRoutingModule } from './imexport-routing.module';
import { ImexportComponent } from './imexport.component';

@NgModule({
  imports: [
    CommonModule,
    ImexportRoutingModule,
    NgbModule.forRoot()
  ],
  declarations: [ImexportComponent]
})
export class ImexportModule { }
