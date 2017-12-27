import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { routerTransition } from '../../router.animations';
import { TokenService, AccountService,  MessageService} from '../shared/services'
import { UserLogin } from '../shared/services/class/user-login'
import { validateConfig } from '@angular/router/src/config';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    animations: [routerTransition()]
})
export class LoginComponent implements OnInit {
    user = new UserLogin();
    model: any = {};
    constructor(
        public router: Router, 
        public tkService : TokenService, 
        public msService : MessageService
        ) {}
 
    ngOnInit() {
        // reset login status
        
    }
 
    login() {
        
        this.tkService.getToken(this.user);
            
    }
   
}
