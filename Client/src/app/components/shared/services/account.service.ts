import { Injectable } from '@angular/core';
import {UserChangePassword} from './class/user-change-password';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router'

import { Promise } from 'q';
import { Token, ResponseMessage, AccountCreate, UserResetPassword } from './class';
import { TokenService } from './token.service';

@Injectable()
export class AccountService {
  private rePasswordUrl = 'api/account';  // URL doi mat khau dang nhap lan dau

  constructor(
    private http: HttpClient, 
    private router : Router, 
    private tkService : TokenService
  ) { }
 

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

  createAccount(acc : AccountCreate):Promise<any>{
    return Promise((resolve,reject) => {
      this.http.post<ResponseMessage>(this.rePasswordUrl,acc).subscribe(
        data=>{
          if(data.code == "200"){
            
            resolve(data.data as UserResetPassword);
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

  addRoleAccount(acc : UserResetPassword,role : string[]):Promise<any>{
    return Promise((resolve,reject) => {
      this.http.put<ResponseMessage>(`${this.rePasswordUrl}/${acc.id}/roles`,role).subscribe(
        data=>{
          if(data.code == "200"){
            
            resolve("Success!");
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

  getCurrentUserFullName(){
    let curAccount;
    curAccount = this.decodeToken();
    return curAccount['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    
  }

  getCurrentUserRole(){
    let curAccount;
    curAccount = this.decodeToken();
    return curAccount['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    
  }

  getCurrentUserName(){
    if(this.tkService.checkToken()){
    let curAccount;
    curAccount = this.decodeToken();
    return curAccount['sub'];
    }
    
  }

  decodeToken(){
    if(this.tkService.checkToken()){
      let tk = this.tkService.getTokenLocal();
      let infString = tk.slice(tk.indexOf('.')+1,tk.lastIndexOf('.'));
      return JSON.parse(this.b64DecodeUnicode(infString));
    }
      
  }

  b64DecodeUnicode(str) {
      // Going backwards: from bytestream, to percent-encoding, to original string.
      return decodeURIComponent(atob(str).split('').map((c) => {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
      }).join(''));
  }
  
}
