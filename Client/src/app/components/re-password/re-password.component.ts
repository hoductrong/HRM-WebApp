import { Component, OnInit } from '@angular/core';
import { TokenService, AccountService,  MessageService} from '../shared/services'
import { Router } from '@angular/router';
import { routerTransition } from '../../router.animations';
import { validateConfig } from '@angular/router/src/config';
import { UserChangePassword } from '../shared/services/class/user-change-password'

@Component({
  selector: 'app-re-password',
  templateUrl: './re-password.component.html',
  styleUrls: ['./re-password.component.scss'],
  animations: [routerTransition()],
})
export class RePasswordComponent implements OnInit {
  user = new UserChangePassword();
  message = '';

  constructor(
    public router: Router, 
    public tkService : TokenService, 
    public accService : AccountService) { 

      if(this.tkService.checkToken){
        this.user.userName = this.accService.getCurrentUserName();
      }
      
    }

  ngOnInit() {
  }

  changePassword(){
            
    this.accService.rePassword(this.user)
    .then(
      data => {
        localStorage.setItem("token",data.token);
        this.router.navigate(['/dashboard']);
      },
      error =>{
        this.message = error;
      }
    )
    
  }
}
