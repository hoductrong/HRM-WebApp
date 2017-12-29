import { Injectable } from '@angular/core';
import { Token, ResponseMessage } from './class'
import { HttpClient } from '@angular/common/http';
import { UserLogin } from './class/user-login';
import { Router } from '@angular/router'

@Injectable()
export class TokenService {
  urlToken:string = 'api/account/token';
  constructor(private http: HttpClient,private router: Router) { }
  token:Token = new Token() ;
  userLogin = new UserLogin();

  setToken(tk:ResponseMessage){
    this.token = tk.data as Token;
      localStorage.setItem("token",this.token.token);
  }

  getTokenLocal(){
    return localStorage.getItem("token");
  }

  checkToken(){
    if(localStorage.getItem("token")) return true;
    else return false;
  }
  
  removeToken(){
    localStorage.removeItem("token");
  }

  getToken(user:UserLogin){
    
    this.http.post<ResponseMessage>(this.urlToken,user).subscribe(
      data => {
        this.setToken(data);
        this.router.navigate(['/dashboard']);
      },
    )
  }
}
