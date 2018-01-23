import { Component, OnInit,NgZone } from '@angular/core';
import { RoleService } from '../../shared/services/role.service'
import { Role } from '../../shared/services/class/index';

@Component({
  selector: 'app-role-manager',
  templateUrl: './role-manager.component.html',
  styleUrls: ['./role-manager.component.scss']
})
export class RoleManagerComponent implements OnInit {
  isHide = true;
  isDisabled = true;
  roles : Role[] =[];
  roleName = '';
  constructor(
    private roleServices : RoleService,
    private zone : NgZone
  ) { }

  ngOnInit() {
     this.roleServices.getRoles()
     .then(
       result =>{
        this.zone.run(() => {
         this.roles = result;
        })
       },
       error => {
         window.alert(error);
       }
     )
  }

  createRole(roleName : string){
    this.roleServices.createRole(roleName)
    .then(
      (result : Role) =>{
        this.zone.run(() => {
          this.roles.push(result);
          window.alert('Thêm quyền thành công');
        })
      },
      error => {
        window.alert(error);
      }
    )
  }

  editRole(role : Role,roleName){
    this.roleServices.editRole(role,roleName)
    .then(
      result =>{
        this.zone.run(() => {
        this.roles[this.roles.findIndex(el => el.id === result.id)] = result;
        })
        window.alert('Sửa quyền thành công');
      },
      error =>{
        window.alert(error);
      }
    )
  }

  deleteRole(role : Role){
    this.roleServices.deleteRole(role)
    .then(
      result =>{
        this.zone.run(() => {
          this.roles = this.deleteInArray(this.roles,role);
        })
      }
    )
  }

  deleteInArray(roleC : Role[],role : Role){
    return roleC.filter(role2 => {
        return role2.id != role.id;
    })
  }


  

}
