import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HttpClient,HTTP_INTERCEPTORS  } from '@angular/common/http';


import { AppComponent } from './app.component';
import {HttpModule} from '@angular/http';
import {FormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './/app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthGuard,MessageService,HttpInterceptorService } from './components/shared';
import { TokenService } from './components/shared/services'
import { RePasswordModule } from './components/re-password/re-password.module';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    CommonModule,
    HttpModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    AppRoutingModule,
    RePasswordModule
  ],
  providers: [AuthGuard,TokenService,MessageService,
    { provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true }  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
