import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeRoutingModule } from './employee-routing.module';
import { EmployeeComponent } from './employee.component';
import { FormsModule } from '@angular/forms';
import { TokenService,HttpInterceptorService,AccountService,MessageService,EmployeeService } from '../../shared/services'


@NgModule({
  imports: [
    CommonModule,
    NgbModule.forRoot(),
    EmployeeRoutingModule,
    FormsModule
    
  ],
  declarations: [EmployeeComponent],
  providers:[MessageService,EmployeeService]
})
export class EmployeeModule { }
