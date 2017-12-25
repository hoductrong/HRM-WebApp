import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../../router.animations';

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.scss'],
  animations: [routerTransition()],
})
export class DocumentComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
