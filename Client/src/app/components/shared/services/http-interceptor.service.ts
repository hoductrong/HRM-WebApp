import { Injectable } from '@angular/core';
import { Router } from '@angular/router'
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpErrorResponse,
  HttpEvent
} from '@angular/common/http';
import {MessageService} from './message.service'
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class HttpInterceptorService implements HttpInterceptor {
  constructor(public router: Router,public msService : MessageService) { }
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const customReq = request.clone({
      
      setHeaders : {
        'Content-Type' : 'application/json',
        'Authorization' : 'Bearer ' + localStorage.getItem("token")
      }
    });
    
    return next
    .handle(customReq)
    .catch(resErr => {
      if (resErr instanceof HttpErrorResponse) {
            
        
        if(resErr.status===403&&resErr.error.errorMessage=="Bạn phải thay đổi mật khẩu cho lần đầu đăng nhập"){
          window.alert(resErr.error.errorMessage);
          this.router.navigate(['/re-password']);
        }

        if(resErr.status===401){
          localStorage.removeItem("token");
          this.router.navigate(['/login']);
        }

        if(resErr.status===404){
          this.router.navigate(['/not-found']);
        }


        if(resErr.error != null){
          this.msService.add(resErr.error.errorMessage);   
        } 
        }
        
      return Observable.throw(resErr);
    });;
  }
}