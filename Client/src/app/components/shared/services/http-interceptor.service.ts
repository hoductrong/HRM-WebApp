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
      headers : request.headers.set('Authorization', 'Bearer ' + localStorage.getItem("token"))
    });
    
    return next
    .handle(customReq)
    .catch(resErr => {
      if (resErr instanceof HttpErrorResponse) {
            
        
        if(resErr.status===403&&resErr.error.errorMessage=="You must change password for login first time"){
          this.router.navigate(['/re-password']);
        }

        if(resErr.status===401){
          localStorage.removeItem("token");
          this.router.navigate(['/login']);
        }

        if(resErr.error != null){
          this.msService.add(resErr.error.errorMessage);   
        } 
        }
        
      return Observable.throw(resErr);
    });;
  }
}