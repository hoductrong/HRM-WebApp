using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNongTrai.Model.Entity
{
    [Table("Harvest")]
    public class Harvest : BaseEntity
    {
        public DateTime GatherTime { get; set; }
        public float Quantity { get; set; }
        //foreign key id
        public Guid LandUsageId { get; set; }
        public Guid SeedId { get; set; }
        //relationship
        public LandUsage LandUsage { get; set; }
        public Seed Seed { get; set; }
    }
}