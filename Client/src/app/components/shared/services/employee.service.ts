import { Injectable } from '@angular/core';
import { Employee, ResponseMessage } from './class';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { MessageService } from './message.service';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable()
export class EmployeeService {
  urlPost : string = 'api/employees';
  employee : Employee = new Employee();
  constructor(
    public http : HttpClient,
    public msgService : MessageService
  ) { }

  createEmployee(emp : Employee){
    this.http.post<ResponseMessage>(this.urlPost,emp).subscribe(
      data=>{
        if(data.code === "200"){
          this.employee = data.data as Employee;
          this.msgService.add("Thêm nhân viên thành công");
          console.log(this.employee);
          
        }
        else {
          this.msgService.add(data.errorMessage);
        }
        
      },
      error=>{
        console.log(error);
      }
    )
    return this.employee;
  }

}
