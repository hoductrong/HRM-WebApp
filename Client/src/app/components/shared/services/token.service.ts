import { Injectable } from '@angular/core';
import { Token } from './class/token'
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserLogin } from './class/user-login';
import { HttpErrorResponse } from '@angular/common/http/src/response';
import { Router } from '@angular/router'
import {MessageService} from './message.service'
@Injectable()
export class TokenService {
  urlToken:string = 'api/account/token';
  constructor(private http: HttpClient,private router: Router,private msService : MessageService) { }
  token:Token = new Token() ;
  userLogin = new UserLogin();

  setToken(tk:Token){
      localStorage.setItem("token",tk.token);
  }
  checkToken(){
    if(localStorage.getItem("token")) return true;
    else return false;
  }
  removeToken(){
    localStorage.removeItem("token");
  }
  getToken(user:UserLogin){
    
    this.http.post<Token>(this.urlToken,user).subscribe(
      data => {
        this.setToken(data);
        this.router.navigate(['/dashboard']);
      },
    )
  }
}
