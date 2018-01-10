import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../../../router.animations';
import { AccountService } from '../../../shared/services';

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.scss'],
    animations: [routerTransition()]
})
export class SidebarComponent implements OnInit {
    role : string;
    constructor(
        public accService : AccountService
    ){
        this.role = this.accService.getCurrentUserRole();
    };

    ngOnInit(){};

    isActive: boolean = false;
    showMenu1: string = 'pages1';
    showMenu2: string = 'pages2';

    eventCalled() {
        this.isActive = !this.isActive;
    }

    addExpandClass(element: any) {
        if (element === this.showMenu1) {
            this.showMenu1 = '0';
        } 
        
        else if (element === this.showMenu2) {
            this.showMenu2 = '0';
        } 
        else if(element==='pages1'){
            this.showMenu1 = element;
        }
        else this.showMenu2 = element;
    }
}
