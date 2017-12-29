import { Component, OnInit, NgZone } from '@angular/core';
import { routerTransition } from '../../../router.animations';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Farmer } from '../../shared/services/class';
import { FarmerService } from '../../shared/services';
@Component({
  selector: 'app-farmer',
  templateUrl: './farmer.component.html',
  animations: [routerTransition()],
  styleUrls: ['./farmer.component.scss']
})
export class FarmerComponent implements OnInit {
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
      private zone : NgZone
    ) { }

  open(content) {
      this.modalService.open(content)
      .result
      .then((result) => {
        let frmr = new Farmer();
        frmr.birthDay = `${this.time1["month"].toString()}-${this.time1["day"].toString()}-${this.time1["year"].toString()} `;
        this.createFarmer(result);
      }, (reason) => {
      });
  }

  ngOnInit() {

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
            window.alert(err);
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

  deleteFarmer(frmr : Farmer){
      this.frmrService.deleteFarmer(frmr)
      .then(
          data => {
            this.zone.run(() => {
                this.frmrCollection = this.deleteInArray(this.frmrCollection,frmr);
                console.log(this.deleteInArray(this.frmrCollection,data));
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

  deleteInArray(frmrC : Farmer[],frmr : Farmer){
    return frmrC.filter(frmr2 => {
        return frmr2.farmerId != frmr.farmerId;
    })
  }
}
