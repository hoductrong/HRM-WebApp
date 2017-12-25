using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNongTrai.Model.Entity
{
    [Table("ResourcesUsage")]
    public class ResourcesUsage : BaseEntity
    {
        [MaxLength(2)]
        [Required]
        public string ResourcesType { get; set; }
        public Guid ResourcesId { get; set; }
        public float Quantity { get; set; }
        public DateTime UsageTime { get; set; }

        //foreign key id
        public Guid LandUsageId { get; set; }
        //relationship
        public LandUsage LandUsage { get; set; }
    }
}