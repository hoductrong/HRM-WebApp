import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient,HTTP_INTERCEPTORS  } from '@angular/common/http';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { FormsModule } from '@angular/forms'
import { TokenService,HttpInterceptorService,AccountService,MessageService } from '../shared/services'


@NgModule({
    imports: [CommonModule, LoginRoutingModule,FormsModule,HttpClientModule],
    declarations: [LoginComponent],
    providers: [TokenService, AccountService,MessageService,
        { provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true }
    ]
})
export class LoginModule {}
