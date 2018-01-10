import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { TokenService } from '../services/token.service';

@Injectable()
export class LoginPreventGuard implements CanActivate {

  constructor(
    private router: Router,
    private tkService:TokenService
  ) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
      if (this.tkService.checkToken()) {
        this.router.navigate(['/dashboard']);
        return false;
    }
    else {
        
        return true;
    }
  
  }
}
