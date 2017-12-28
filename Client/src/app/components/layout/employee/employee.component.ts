import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../../router.animations';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Employee } from '../../shared/services/class'
import { EmployeeService, MessageService } from '../../shared/services'

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  animations: [routerTransition()],
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent implements OnInit {
  temp : Employee;
  emp : Employee = new Employee();
  constructor(
      private modalService: NgbModal, 
      private empService : EmployeeService,
      private msgService : MessageService
    ) { }

  open(content) {
    this.modalService.open(content)
      .result
      .then((result) => {
          this.emp = this.empService.createEmployee(result);
          window.alert(this.msgService.messages)
      }, (reason) => {
          
      });
  }

  ngOnInit() {
  }

}
