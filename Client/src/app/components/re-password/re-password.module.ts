import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient,HTTP_INTERCEPTORS  } from '@angular/common/http';

import { RePasswordRoutingModule } from './re-password-routing.module';
import { RePasswordComponent } from './re-password.component'
import { FormsModule } from '@angular/forms'
import { TokenService,HttpInterceptorService,AccountService,MessageService } from '../shared/services'

@NgModule({
  imports: [
    CommonModule,
    RePasswordRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  declarations: [RePasswordComponent],
  providers: [TokenService, AccountService,MessageService,
    { provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true }]
})
export class RePasswordModule { }
