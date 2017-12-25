using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNongTrai.Model.Entity
{
    [Table("FertilizerTypes")]
    public class FertilizerType : BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        //relationship
        public ICollection<Fertilizer> Fertilizers {get;set;}
    }
}