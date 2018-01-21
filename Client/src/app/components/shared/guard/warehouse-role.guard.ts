import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AccountService } from '../services';
import { Router } from '@angular/router';

@Injectable()
export class WarehouseRoleGuard implements CanActivate {
  constructor(
    private router: Router,
    private accService: AccountService) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (this.accService.getCurrentUserRole() == "warehouse") {
      return true;
    }
    else {
      this.router.navigate(['/dashboard']);
      return false;
    }
  }
}
