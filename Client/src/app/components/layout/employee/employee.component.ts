import { Component, OnInit, NgZone } from '@angular/core';
import { routerTransition } from '../../../router.animations';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Employee, AccountCreate } from '../../shared/services/class';
import { EmployeeService, AccountService } from '../../shared/services';

@Component({

  selector: 'app-employee',
  templateUrl: './employee.component.html',
  animations: [routerTransition()],
  styleUrls: ['./employee.component.scss'],
})
export class EmployeeComponent implements OnInit {
    accountName = "";
    isDisabled = false;
    p : Number = 1;
    emp : Employee = new Employee();
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
  filter : Employee = new Employee();
  empCollection : Employee[] = new Array<Employee>();

  constructor(
      private modalService: NgbModal, 
      private empService : EmployeeService,
      private accService : AccountService,
      private zone : NgZone
    ) { }
     
  open(content) {
    this.isDisabled = false;
    this.emp = new Employee();
      this.emp.haveAccount = true;
    this.modalService.open(content)
      .result
      .then((result) => {
          
          result.birthday = `${this.time1["month"].toString()}-${this.time1["day"].toString()}-${this.time1["year"].toString()} `;
          result.startWorkTime = `${this.time2["month"].toString()}-${this.time2["day"].toString()}-${this.time2["year"].toString()} `;
          this.createEmployee(result);
    }, (reason) => {
          
    });
  }

  openEditMenu(emp : Employee,content){
    this.emp = emp;
    this.emp.haveAccount = true;
    this.time1['year'] = parseInt(emp.birthDay.substr(0,4));
    this.time1['month'] = parseInt(emp.birthDay.substr(5,2));
    this.time1['day'] = parseInt(emp.birthDay.substr(8,2));
    this.isDisabled = false;
    this.modalService.open(content)
    .result
    .then((result) => {
      this.editEmployee(result);
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

  watchEmployee(emp : Employee,content){

    this.emp = emp;
      this.time1['year'] = parseInt(emp.birthDay.substr(0,4));
      this.time1['month'] = parseInt(emp.birthDay.substr(5,2));
      this.time1['day'] = parseInt(emp.birthDay.substr(8,2));
      this.isDisabled = true;
      this.modalService.open(content)
      .result
      .then((result) => {
        let acc : AccountCreate = new AccountCreate();
        acc.personalId = emp.personalId;
        acc.userName = this.accountName;
          
        this.accService.createAccount(acc)
        .then(
            result => {
                this.addRole(result);
                window.alert(`Tên tài khoản: ${result.userName} . Mật khẩu: ${result.password}`);
            },
            error =>{
                window.alert(error);
            }
        )
        
      }, (reason) => {
      });
}

addRole(result){
    let role : string[] = ['manager'];
    this.accService.addRoleAccount(result,role)
    .then(
        result =>{
        },
        error =>{
            window.alert(error);
        }
    )
}

editEmployee(emp : Employee){
  this.empService.editEmployee(emp)
        .then(
            data => {
              this.zone.run(() => {
                  window.alert('Sửa nhân viên thành công');
                  this.empCollection[this.empCollection.findIndex(el => el.employeeId === data.employeeId)] = data;

              });
            },
            error =>{
                window.alert(error);
            }

        )
    }  

}

