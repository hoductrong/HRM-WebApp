using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNongTrai.Model.Entity
{
    [Table("ResourcesExport")]
    public class ResourcesExport : BaseEntity
    {
        [MaxLength(2)]
        [Required]
        public string ResourcesType { get; set; }
        public Guid ResourcesId { get; set; }
        public DateTime ExportTime { get; set; }
        public float Quantity { get; set; }
        //foreign key id
        public Guid FamerId { get; set; }
        //relationship
        public Famer Famer { get; set; }
    }
}