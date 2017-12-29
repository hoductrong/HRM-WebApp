import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule, HttpClient,HTTP_INTERCEPTORS  } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { FilterPipeModule } from 'ngx-filter-pipe';
import {NgxPaginationModule} from 'ngx-pagination';
import { TokenService, FarmerService, HttpInterceptorService,AccountService,MessageService } from '../../shared/services';

import { FarmerRoutingModule } from './farmer-routing.module';
import { FarmerComponent } from './farmer.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgbModule.forRoot(),
    FarmerRoutingModule,
    HttpClientModule,
    NgxPaginationModule,
    FilterPipeModule
  ],
  declarations: [FarmerComponent],
  providers:[
    MessageService,FarmerService,
    { provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true },
  ]
})
export class FarmerModule { }
