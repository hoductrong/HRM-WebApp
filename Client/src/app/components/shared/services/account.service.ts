import { Injectable } from '@angular/core';
import {UserChangePassword} from './class/user-change-password';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router'

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';
import { TokenService } from './token.service';
import { MessageService } from './message.service';
import { Token, ResponseMessage } from './class'

@Injectable()
export class AccountService {
  private rePasswordUrl = 'api/account';  // URL doi mat khau dang nhap lan dau

  constructor(private http: HttpClient, private router : Router, private tkService:TokenService, private msgService:MessageService) { }
  /** PUT: doi mat khau lan dau */
  rePassword (user: UserChangePassword) {
    this.http.put<ResponseMessage>(this.rePasswordUrl, user).subscribe(
      data=>{
        if(data.code ==="200"){
          this.tkService.setToken(data);
          this.router.navigate(['/dashboard']);
        }
        else {
          this.msgService.add(data.errorMessage);
        }
        
      },
      error=>{
        console.log(error);
      }
    )
  }
  
}
