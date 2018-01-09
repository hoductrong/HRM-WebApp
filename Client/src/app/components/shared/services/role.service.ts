import { Injectable } from '@angular/core';
import { Role,ResponseMessage } from './class';
import { Promise } from 'q';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class RoleService {
  urlRole : string = "api/roles"
  constructor(
    public http : HttpClient,
  ) { }

  getRoles():Promise<any>{
    
    return Promise((resolve,reject) => {
      this.http.get<ResponseMessage>(this.urlRole).subscribe(
        data=>{
          if(data.code == "200"){
            
            resolve(data.data as Role[]);
            
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

  createRole(rolename : string):Promise<any>{
    let roleName = {
      name : rolename
    }
    
    return Promise((resolve,reject) => {
      this.http.post<ResponseMessage>(this.urlRole,roleName).subscribe(
        data=>{
          if(data.code == "200"){
            
            resolve(data.data as Role);
            
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

  deleteRole(role : Role):Promise<any>{
    
    return Promise((resolve,reject) => {
      this.http.delete<ResponseMessage>(`${this.urlRole}/${role.id}`).subscribe(
        data=>{
          if(data.code == "200"){
            
            resolve(data.data);
            
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

  editRole(role : Role,rolename : string):Promise<any>{
    let roleName = {
      name : rolename
    }
    
    return Promise((resolve,reject) => {
      this.http.put<ResponseMessage>(`${this.urlRole}/${role.id}`,roleName).subscribe(
        data=>{
          if(data.code == "200"){
            
            resolve(data.data as Role);
            
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
