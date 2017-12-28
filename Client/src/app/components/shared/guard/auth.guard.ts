import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Router } from '@angular/router';
import { TokenService } from '../services/token.service'

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private router: Router,private tkService:TokenService) {}

    canActivate() {
        if (this.tkService.checkToken()) {
            return this.tkService.checkToken()
        }
        else {
            this.router.navigate(['/login']);
            return false;
        }
    }
    
}
