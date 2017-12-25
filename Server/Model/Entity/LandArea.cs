using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNongTrai.Model.Entity
{
    [Table("LandAreas")]
    public class LandArea : BaseEntity
    {
        [MaxLength(6)]
        [Required]
        public string LandCode { get; set; }
        public DateTime AddedTime { get; set; }
        [MaxLength(20)]
        public string Latitude { get; set; }
        [MaxLength(20)]
        public string Longitude { get; set; }
        public bool OfCompany { get; set; }
        //relationship
        public LandUsage LandUsage { get; set; }
    }
}