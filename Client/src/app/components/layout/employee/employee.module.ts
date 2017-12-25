import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeRoutingModule } from './employee-routing.module';
import { EmployeeComponent } from './employee.component';


@NgModule({
  imports: [
    CommonModule,
    NgbModule.forRoot(),
    EmployeeRoutingModule,
    
  ],
  declarations: [EmployeeComponent]
})
export class EmployeeModule { }
