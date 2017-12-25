using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNongTrai.Model.Entity
{
    [Table("LandUsage")]
    public class LandUsage : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //foreign key id
        public Guid LandAreaId { get; set; }
        public Guid FamerId { get; set; }
        //relationship
        public LandArea LandArea { get; set; }
        public Famer Famer { get; set; }
        public ICollection<ResourcesUsage> ResourcesUsages { get; set; }
        public ICollection<Harvest> Harvests { get; set; }
    }

}