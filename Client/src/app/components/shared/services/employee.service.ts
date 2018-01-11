import { Injectable } from '@angular/core';
import { Employee, ResponseMessage } from './class';
import { HttpClient } from '@angular/common/http';
import { MessageService } from './message.service';

import { Observable } from 'rxjs/Observable';
import { Promise } from 'q';

@Injectable()
export class EmployeeService {
  urlEmp : string = 'api/employees';
  constructor(
    public http : HttpClient,
    public msgService : MessageService
  ) { }

  createEmployee(emp : Employee):Promise<any>{
    
    return Promise((resolve,reject) => {
      this.http.post<ResponseMessage>(this.urlEmp,emp).subscribe(
        data=>{
          if(data.code == "200"){
            
            resolve(data.data as Employee);
            
          }
          else {
            this.msgService.add(data.errorMessage);
            reject(data.errorMessage);
          }
          
        },
        error=>{
          reject(error);
        }
      )
    }) 
  }

  deleteEmployees(emp : Employee):Promise<any>{
    
    return Promise((resolve,reject) => {
      this.http.delete<ResponseMessage>(`${this.urlEmp}/${emp.employeeId}`).subscribe(
        data => {
          if(data.code == "200"){
            
            resolve(data.data as Employee);
            
          }
          else {
            reject(data.errorMessage);
          }
          
        },
        error=>{
          reject(error);
        }
      )
    }) 
  }

  editEmployee(emp : Employee):Promise<any>{
    
    return Promise((resolve,reject) => {
      this.http.put<ResponseMessage>(`${this.urlEmp}/${emp.employeeId}`,emp).subscribe(
        data => {
          if(data.code == "200"){
            
            resolve(data.data as Employee);
            
          }
          else {
            reject(data.errorMessage);
          }
          
        },
        error=>{
          reject(error);
        }
      )
    }) 
  }

  getEmployees():Promise<any>{
    
    return Promise((resolve,reject) => {
      this.http.get<ResponseMessage>(this.urlEmp).subscribe(
        data=>{
          if(data.code == "200"){
            
            resolve(data.data as Employee[]);
            
          }
          else {
            reject(data.errorMessage);
          }
          
        },
        error=>{
          reject(error);
        }
      )
    }) 
  }

}
