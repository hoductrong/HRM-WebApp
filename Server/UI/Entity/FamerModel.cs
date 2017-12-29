using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai.UI.Entity
{
    public class FamerModel {
        [BindNever]
        public Guid FamerId {get;set;}
        [BindNever]
        public Guid PersonalId {get;set;}
        [MaxLength(50)]
        [Required]
        public string FullName {get;set;}
        [MaxLength(100)]
        public string Address {get;set;}
        [Range(0,1)]
        [Required]
        public int Sex {get;set;}
        public DateTime? BirthDay {get;set;}
        [MaxLength(11)]
        [Required]
        public string Phone {get;set;}
        public string Description {get;set;}

        public FamerModel(){}
        /// <summary>
        /// Create FamerModel from Famer object
        /// </summary>
        /// <param name="famer"></param>
        /// <returns></returns>
        public static FamerModel GetModel(Famer famer){
            if(famer.Personal == null)
                return null;
            FamerModel model = new FamerModel();
            model.FamerId = famer.Id;
            model.PersonalId = famer.PersonalId;
            model.FullName = famer.Personal.FullName;
            model.Address = famer.Personal.Address;
            model.Sex = famer.Personal.Sex == true ? (int)SexEnum.Male : (int)SexEnum.Female;
            model.BirthDay = famer.Personal.BirthDay;
            model.Phone = famer.Personal.Phone;
            model.Description = famer.Personal.Description;
            return model;
        }
        /// <summary>
        /// Create Famer object from FamerModel
        /// </summary>
        /// <returns></returns>
        public Famer CreateEntity(){
            Famer famer = new Famer();
            famer.Id = this.FamerId;
            famer.PersonalId = this.PersonalId;
            famer.Personal = new Personal();
            famer.Personal.Id = this.PersonalId;
            famer.Personal.FullName = this.FullName;
            famer.Personal.Address = this.Address;
            famer.Personal.Sex = this.Sex == (int)SexEnum.Male ? true : false;
            famer.Personal.BirthDay = this.BirthDay;
            famer.Personal.Phone = this.Phone;
            famer.Personal.Description = this.Description;
            return famer;
        }
    }
}