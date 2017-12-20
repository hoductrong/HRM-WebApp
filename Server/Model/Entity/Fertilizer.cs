using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNongTrai.Model.Entity
{
    [Table("Fertilizers")]
    public class Fertilizer : BaseEntity {
        [MaxLength(50)]
        [Required]
        public string Name {get;set;}
        [MaxLength(50)]
        [Required]
        public string Manufacturer {get;set;}

        //foreign key id
        public Guid FertilizerTypeId {get;set;}
        //relationship
        public FertilizerType FertilizerType {get;set;}
    }
}