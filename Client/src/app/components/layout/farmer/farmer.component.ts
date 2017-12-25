import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../../router.animations';

@Component({
  selector: 'app-farmer',
  templateUrl: './farmer.component.html',
  animations: [routerTransition()],
  styleUrls: ['./farmer.component.scss']
})
export class FarmerComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
