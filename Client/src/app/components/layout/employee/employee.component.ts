import { Component, OnInit, NgZone } from '@angular/core';
import { routerTransition } from '../../../router.animations';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Employee } from '../../shared/services/class';
import { EmployeeService } from '../../shared/services';
import { ChangeDetectorRef,ChangeDetectionStrategy } from '@angular/core';

@Component({

  selector: 'app-employee',
  templateUrl: './employee.component.html',
  animations: [routerTransition()],
  styleUrls: ['./employee.component.scss'],
})
export class EmployeeComponent implements OnInit {
    p : Number = 1;
  time1 : object = {
    "year": 1990,
    "month": 1,
    "day": 1
  };
  time2 : object = {
    "year": 1990,
    "month": 1,
    "day": 1
  };
  temp : Employee = new Employee();
  emp : Employee = new Employee();
  filter : Employee = new Employee();
  notificate : string;
  empCollection : Employee[] = new Array<Employee>();

  constructor(
      private modalService: NgbModal, 
      private empService : EmployeeService,
      private ref : ChangeDetectorRef,
      private zone : NgZone
    ) {
        //this.getAllEmployees();
     }
     
  open(content) {
    this.modalService.open(content)
      .result
      .then((result) => {
          this.emp.birthday = `${this.time1["month"].toString()}-${this.time1["day"].toString()}-${this.time1["year"].toString()} `;
          this.emp.startWorkTime = `${this.time2["month"].toString()}-${this.time2["day"].toString()}-${this.time2["year"].toString()} `;
          this.createEmployee(result);
    }, (reason) => {
          
    });
  }

  ngOnInit() {
    this.getAllEmployees();
  }

  getAllEmployees(){
    this.empService.getEmployees()
    .then(
        data => {
            this.zone.run(() => {
            this.empCollection = data;
            });
            
        },
        err => {
            window.alert(err);
        }
    )
  }

  createEmployee(emp : Employee){
    this.empService.createEmployee(emp)
          .then(
              data => {
                this.zone.run(() => {
                    window.alert('Thêm nhân viên thành công');
                    this.empCollection.push(data);
                    });
              },
              error =>{
                  window.alert(error);
              }

          )
      
  }

  deleteEmpoyee(emp : Employee){
      this.empService.deleteEmployees(emp)
      .then(
          data => {
            this.zone.run(() => {
                this.empCollection = this.deleteInArray(this.empCollection,emp);
                console.log(this.deleteInArray(this.empCollection,data));
                });
            
          },
          err => {
              window.alert(err);
          }
      )
  }

  isSex(num:number){
    if(num===1) return 'Nam';
    else return 'Nữ';
  }

  deleteInArray(empC : Employee[],emp : Employee){
    return empC.filter(emp2 => {
        return emp2.employeeId != emp.employeeId;
    })
  }
}
