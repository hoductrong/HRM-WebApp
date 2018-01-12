import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient,HTTP_INTERCEPTORS  } from '@angular/common/http';
import { AccountInfoRoutingModule } from './account-info-routing.module';
import { AccountInfoComponent } from './account-info.component';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../shared/services'


@NgModule({
  imports: [
    CommonModule,
    AccountInfoRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  declarations: [AccountInfoComponent],
  providers : [AccountService]
})
export class AccountInfoModule { }
