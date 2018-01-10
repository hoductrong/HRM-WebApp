import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import {AccountService } from '../services';

@Injectable()
export class FarmerRoleGuard implements CanActivate {
  constructor(private router: Router,private accService:AccountService) {}
  canActivate(){
    if (this.accService.getCurrentUserRole()=="farmer") {
      return true;
    }
    else {
        this.router.navigate(['/dashboard']);
        return false;
    }
  }
}
