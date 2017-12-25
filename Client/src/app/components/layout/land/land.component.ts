import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../../router.animations';

@Component({
  selector: 'app-land',
  templateUrl: './land.component.html',
  styleUrls: ['./land.component.scss'],
  animations: [routerTransition()],
})
export class LandComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
