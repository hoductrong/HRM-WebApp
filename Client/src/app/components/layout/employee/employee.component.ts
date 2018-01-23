import { Component, OnInit, NgZone } from '@angular/core';
import { routerTransition } from '../../../router.animations';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Employee, AccountCreate } from '../../shared/services/class';
import { EmployeeService, AccountService } from '../../shared/services';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({

  selector: 'app-employee',
  templateUrl: './employee.component.html',
  animations: [routerTransition()],
  styleUrls: ['./employee.component.scss'],
})
export class EmployeeComponent implements OnInit {
    inputsForm : FormGroup;
    accountName = "";
    isDisabled = false;
    isHumanResources = false;
    isWareHouse = false;
    isFarmer = false;
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
      private zone : NgZone,
      private fb:FormBuilder
    ) { }
     
  open(content) {
    this.isDisabled = false;
    this.emp = new Employee();
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
    this.time1['year'] = parseInt(emp.birthDay.substr(0,4));
    this.time1['month'] = parseInt(emp.birthDay.substr(5,2));
    this.time1['day'] = parseInt(emp.birthDay.substr(8,2));
    this.getRoleFromEmployee(emp);
    this.isDisabled = false;
    this.modalService.open(content)
    .result
    .then((result) => {
      this.editEmployee(result);
      this.editRole(result);
      
    }, (reason) => {
    });
}

  ngOnInit(){
    this.getAllEmployees();
    this.inputsForm=this.fb.group({

        phone: [this.emp.phone,[Validators.required,this.checkPhoneNum]],
        phone_disabled: [{value : this.emp.phone, disabled : true}],
        fullname:[],
        fullname_disabled:[{value : this.emp.phone, disabled : true}],
        address:[],
        address_disabled:[{value : this.emp.phone, disabled: true}],
        salary:[this.emp.salary,[Validators.required,this.checkSalary]],
        salary_disabled: [{value: this.emp.salary, disabled : true}],
        sex:[],
        sex_disabled:[{value: this.emp.sex, disabled: true}],
        birthday:[this.emp.birthDay,[Validators.required,this.checkbirthday]],
        startworktime:[this.emp.startWorkTime,[Validators.required,this.checkstartworktime]],
        accountname:[],

        });

      
  }

  
  checkPhoneNum(control:FormControl){
    if(!isNaN(control.value)){
      return{validphone:false};
    }
    return {validphone:true};
  }
  
  

  checkSalary(control:FormControl){
    if(!isNaN(control.value)){
      return{validsalary:false};
    }
    return {validsalary:true};
  }

  checkbirthday(control:FormControl){
    if(control.value instanceof Object){
      return{validbirthday:false};
    }
    return {validbirthday:true};
  }

  checkstartworktime(control:FormControl){
    if(control.value instanceof Object){
      return{validstartworktime:false};
    }
    return {validstartworktime:true};
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
            
        }
    )
  }

  getRoleFromEmployee(emp : Employee){
    this.empService.getUserId(emp)
    .then(
    result => {
      this.accService.getRoleAccount(result.userId)
      .then(
        (result : string[]) => {
          this.zone.run(() => {
                result.forEach(
                element => {
                if(element == 'farmer') this.isFarmer = true;
                else this.isFarmer = false;
                if(element == 'warehouse') this.isWareHouse = true;
                else this.isWareHouse = false;
                if(element == 'humanresources') this.isHumanResources = true;
                this.isWareHouse = false;
              }
            )
            });
          
          
        }
      )
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
                this.addRole(result.id);
                window.alert(`Tên tài khoản: ${result.userName} . Mật khẩu: ${result.password}`);
                this.emp.haveAccount = true;
                this.accountName = '';
            },
            error =>{
                window.alert(error);
            }
        )
        
      }, (reason) => {
      });
}

getRoleFromCheckBox(){
  let role : string[] = [];
  
  if(this.isFarmer) role.push('farmer');
  if(this.isHumanResources) role.push('humanresources');
  if(this.isWareHouse) role.push('warehouse');

  return role;

}

editRole(emp : Employee){
  if(emp.haveAccount&&!this.isDisabled){
  this.empService.getUserId(emp)
  .then(
    result => {
      this.accService.addRoleAccount(result.userId,this.getRoleFromCheckBox())
    },
    err => {
      window.alert(err);
    }
  )
}
}

addRole(userId){
    this.accService.addRoleAccount(userId,this.getRoleFromCheckBox())
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

