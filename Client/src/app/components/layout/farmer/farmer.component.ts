import { Component, OnInit, NgZone } from '@angular/core';
import { routerTransition } from '../../../router.animations';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Farmer, AccountCreate, UserResetPassword } from '../../shared/services/class';
import { FarmerService,AccountService } from '../../shared/services';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-farmer',
  templateUrl: './farmer.component.html',
  animations: [routerTransition()],
  styleUrls: ['./farmer.component.scss']
})
export class FarmerComponent implements OnInit {
    isFormValid = false;
    inputsForm : FormGroup;
    accountName = "";
    isHumanResources = false;
    isWareHouse = false;
    isFarmer = false;
    isDisabled = false;
    p : Number = 1;
    farmer : Farmer = new Farmer();
    time1 : object = {
      "year": 1990,
      "month": 1,
      "day": 1
    };
    filter : Farmer = new Farmer();
    frmrCollection : Farmer[] = new Array<Farmer>();
  constructor(
      private modalService: NgbModal,
      private frmrService : FarmerService,
      private accService : AccountService,
      private zone : NgZone,
      private fb:FormBuilder,
      
    ) { 
        
    }

  open(content) {
      this.isDisabled = false;
      this.farmer = new Farmer();
      
      this.modalService.open(content)
      .result
      .then((result) => {
        result.birthDay = `${this.time1["month"].toString()}-${this.time1["day"].toString()}-${this.time1["year"].toString()} `;
        this.createFarmer(result);
        this.farmer.haveAccount = true;
      }, (reason) => {
      });
  }

  openEditMenu(frmr : Farmer,content){
      this.farmer = frmr;
      this.time1['year'] = parseInt(frmr.birthDay.substr(0,4));
      this.time1['month'] = parseInt(frmr.birthDay.substr(5,2));
      this.time1['day'] = parseInt(frmr.birthDay.substr(8,2));
      this.getRoleFromFarmer(frmr);
      this.isDisabled = false;
      this.modalService.open(content)
      .result
      .then((result) => {
        this.editFarmer(result);
        this.editRole(result);
      }, (reason) => {
      });
  }

  ngOnInit() {
    this.getAllFarmers();
    this.inputsForm=this.fb.group({

        phone: [this.farmer.phone,[Validators.required,this.checkPhoneNum]],
        phone_disabled: [{value: this.farmer.phone, disabled: true}],
        fullname:[this.farmer.fullName,[Validators.required]],
        fullname_disabled:[{value : this.farmer.fullName, disabled : true}],
        address:[this.farmer.address,[Validators.required]],
        address_disabled:[{value : this.farmer.address, disabled : true}],
        sex:[],
        sex_disabled:[{value : this.farmer.sex, disabled : true}],
        birthday:[this.time1,[Validators.required,this.checkbirthday]],
        accountname:[],
        
        });
  }

  checkPhoneNum(control:FormControl){
    if(!isNaN(control.value)){
      return{validphone:false};
    }
    return {validphone:true};
  }

  

  checkbirthday(control:FormControl){
    if(control.value instanceof Object){
      return{validbirthday:false};
    }
    return {validbirthday:true};
  }

  getAllFarmers(){
    this.frmrService.getFarmers()
    .then(
        data => {
            this.zone.run(() => {
            this.frmrCollection = data;
            });
            
        },
        err => {
            
        }
    )
  }

  createFarmer(frmr : Farmer){
    this.frmrService.createFarmer(frmr)
          .then(
              data => {
                this.zone.run(() => {
                    window.alert('Thêm nông dân thành công');
                    this.frmrCollection.push(data);
                    });
              },
              error =>{
                  window.alert(error);
              }

          )
      
  }

  editFarmer(frmr : Farmer){
    this.frmrService.editFarmer(frmr)
          .then(
              data => {
                this.zone.run(() => {
                    window.alert('Sửa nông dân thành công');
                    this.frmrCollection[this.frmrCollection.findIndex(el => el.famerId === data.famerId)] = data;

                });
              },
              error =>{
                  window.alert(error);
              }

          )
      
  } 
  
  getRoleFromFarmer(frmr : Farmer){
    this.frmrService.getUserId(frmr)
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

  deleteFarmer(frmr : Farmer){
      this.frmrService.deleteFarmer(frmr)
      .then(
          data => {
            this.zone.run(() => {
                this.frmrCollection = this.deleteInArray(this.frmrCollection,frmr);
                });
            
          },
          err => {
              window.alert(err);
          }
      )
  }

  watchFarmer(frmr : Farmer,content){

    this.farmer = frmr;
      this.time1['year'] = parseInt(frmr.birthDay.substr(0,4));
      this.time1['month'] = parseInt(frmr.birthDay.substr(5,2));
      this.time1['day'] = parseInt(frmr.birthDay.substr(8,2));
      this.isDisabled = true;
      this.modalService.open(content)
      .result
      .then((result) => {
        let acc : AccountCreate = new AccountCreate();
        acc.personalId = frmr.personalId;
        acc.userName = this.accountName;
          
        this.accService.createAccount(acc)
        .then(
            result => {
                this.addRole(result.id);
                window.alert(`Tên tài khoản: ${result.userName} . Mật khẩu: ${result.password}`);
                this.farmer.haveAccount = true;
                this.accountName = '';
            },
            error =>{
                window.alert(error);
            }
        )
        
      }, (reason) => {
      });
}

addRole(result){
    let role : string[] = ['farmer'];
    this.accService.addRoleAccount(result,role)
    .then(
        result =>{
        },
        error =>{
            window.alert(error);
        }
    )
}

getRoleFromCheckBox(){
    let role : string[] = [];
    
    if(this.isFarmer) role.push('farmer');
    if(this.isHumanResources) role.push('humanresources');
    if(this.isWareHouse) role.push('warehouse');
  
    return role;
  
  }
  
  editRole(frmr : Farmer){
    if(frmr.haveAccount&&!this.isDisabled){
    this.frmrService.getUserId(frmr)
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

  isSex(num:number){
    if(num===1) return 'Nam';
    else return 'Nữ';
  }


  deleteInArray(frmrC : Farmer[],frmr : Farmer){
    return frmrC.filter(frmr2 => {
        return frmr2.famerId != frmr.famerId;
    })
  }
}
