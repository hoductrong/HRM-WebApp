import { Injectable } from '@angular/core';
import { ResponseMessage, Farmer } from './class';
import { HttpClient } from '@angular/common/http';
import { MessageService } from './message.service';

import { Observable } from 'rxjs/Observable';
import { Promise } from 'q';

@Injectable()
export class FarmerService {
  urlFrmr : string = 'api/famers';
  constructor(
    public http : HttpClient,
    public msgService : MessageService
  ) { }
  createFarmer(frmr : Farmer):Promise<any>{
    
    return Promise((resolve,reject) => {
      this.http.post<ResponseMessage>(this.urlFrmr,frmr).subscribe(
        data=>{
          if(data.code == "200"){
            
            resolve(data.data as Farmer);
            
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

  deleteFarmer(frmr : Farmer):Promise<any>{
    
    return Promise((resolve,reject) => {
      this.http.delete<ResponseMessage>(`${this.urlFrmr}/${frmr.famerId}`).subscribe(
        data => {
          if(data.code == "200"){
            
            resolve(data.data as Farmer);
            
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

  editFarmer(frmr : Farmer):Promise<any>{
    
    return Promise((resolve,reject) => {
      this.http.put<ResponseMessage>(`${this.urlFrmr}/${frmr.famerId}`,frmr).subscribe(
        data => {
          if(data.code == "200"){
            
            resolve(data.data as Farmer);
            
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

  getFarmers():Promise<any>{
    
    return Promise((resolve,reject) => {
      this.http.get<ResponseMessage>(this.urlFrmr).subscribe(
        data=>{
          if(data.code == "200"){
            
            resolve(data.data as Farmer[]);
            
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
