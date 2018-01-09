import { Injectable } from '@angular/core';
import {UserChangePassword} from './class/user-change-password';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router'

import { Promise } from 'q';
import { Token, ResponseMessage } from './class'

@Injectable()
export class AccountService {
  private rePasswordUrl = 'api/account';  // URL doi mat khau dang nhap lan dau

  constructor(private http: HttpClient, private router : Router) { }
  /** PUT: doi mat khau lan dau */

  rePassword(user: UserChangePassword):Promise<any>{
    
    return Promise((resolve,reject) => {
      this.http.put<ResponseMessage>(this.rePasswordUrl,user).subscribe(
        data=>{
          if(data.code == "200"){
            
            resolve(data.data as Token);
          }
          else {
            reject(data.errorMessage);
          }
          
        },
        error=>{
          reject(error);
        }
      )
    }) 
  }
  
}
