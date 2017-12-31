import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { TokenService } from '../../../shared/services/token.service'

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    pushRightClass: string = 'push-right';
    personInfo : object;
    constructor(public router: Router,public tkService: TokenService) {
        this.decodeToken();
        this.router.events.subscribe(val => {
            if (
                val instanceof NavigationEnd &&
                window.innerWidth <= 992 &&
                this.isToggled()
            ) {
                this.toggleSidebar();
            }
        });
    }

    decodeToken(){
        let tk = this.tkService.getTokenLocal();
        let infString = tk.slice(tk.indexOf('.')+1,tk.lastIndexOf('.'));
        this.personInfo = JSON.parse(atob(infString));
    }

    isRole(role : string){
        if(role == "manager") return "Quản trị viên";
        if(role == "farmer") return "Nông dân";
    }

    ngOnInit() {
        
    }

    isToggled(): boolean {
        const dom: Element = document.querySelector('body');
        return dom.classList.contains(this.pushRightClass);
    }

    toggleSidebar() {
        const dom: any = document.querySelector('body');
        dom.classList.toggle(this.pushRightClass);
    }

    rltAndLtr() {
        const dom: any = document.querySelector('body');
        dom.classList.toggle('rtl');
    }

    onLoggedout() {
        this.tkService.removeToken();
    }

    
}
