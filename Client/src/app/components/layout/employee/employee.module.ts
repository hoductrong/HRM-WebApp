import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeRoutingModule } from './employee-routing.module';
import { HttpClientModule, HttpClient,HTTP_INTERCEPTORS  } from '@angular/common/http';
import { EmployeeComponent } from './employee.component';
import { FormsModule } from '@angular/forms';
import { FilterPipeModule } from 'ngx-filter-pipe';
import {NgxPaginationModule} from 'ngx-pagination';
import { TokenService,HttpInterceptorService,AccountService,MessageService,EmployeeService } from '../../shared/services'
import { DateFormatter } from '../components/class/date-formatter'

@NgModule({
  imports: [
    CommonModule,
    NgbModule.forRoot(),
    EmployeeRoutingModule,
    FormsModule,
    HttpClientModule,
    NgxPaginationModule,
    FilterPipeModule
  ],
  declarations: [EmployeeComponent],
  providers:[MessageService,EmployeeService,
    { provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true },
    { provide: DateFormatter, 
      useFactory: () => { return new DateFormatter("mm-dd-yyyy") } 
    }
  ]
})
export class EmployeeModule { }
