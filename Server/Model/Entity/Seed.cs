using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNongTrai.Model.Entity
{
    [Table("Seeds")]
    public class Seed : BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        //relationship
        public ICollection<Harvest> Harvests { get; set; }
        public ICollection<SeedProfile> SeedProfiles { get; set; }
    }
}