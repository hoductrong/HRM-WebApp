import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../../router.animations';

@Component({
  selector: 'app-resource',
  templateUrl: './resource.component.html',
  styleUrls: ['./resource.component.scss'],
  animations: [routerTransition()],
})
export class ResourceComponent implements OnInit {
  constructor() { }

  ngOnInit() {
  }

}
