import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../../router.animations';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.scss'],
  animations: [routerTransition()]
})
export class AccountInfoComponent implements OnInit {
  user : object;

  constructor() { }

  ngOnInit() {
  }

}
