using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNongTrai.Model.Entity
{
    [Table("SeedProfile")]
    public class SeedProfile : BaseEntity
    {
        //foreign key id
        public Guid SeedId { get; set; }
        public Guid ProfileDataId { get; set; }
        //relationship
        public Seed Seed { get; set; }
        public ProfileData ProfileData { get; set; }
    }
}