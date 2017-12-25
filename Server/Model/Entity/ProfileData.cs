using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNongTrai.Model.Entity
{
    [Table("ProfileDatas")]
    public class ProfileData : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Url { get; set; }
        //relationship
        public ICollection<SeedProfile> SeedProfiles { get; set; }
    }
}