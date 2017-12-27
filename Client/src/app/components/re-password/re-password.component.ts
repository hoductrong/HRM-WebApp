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
  //luu thong tin mat khau cua user
  user = new UserChangePassword();

  constructor(
    public router: Router, 
    public tkService : TokenService, 
    public msService : MessageService,
    public accService : AccountService) { }

  ngOnInit() {
  }

  changePassword(){
    this.accService.rePassword(this.user);
  }
}