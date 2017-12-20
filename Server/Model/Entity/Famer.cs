using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNongTrai.Model.Entity
{
    [Table("Famers")]
    public class Famer : BaseEntity
    {
        //foreign key id
        public Guid PersonalId { get; set; }
        //relationship
        public Personal Personal { get; set; }
        public ICollection<LandUsage> LandUsages { get; set; }
        public ICollection<ResourcesExport> ResourcesExports { get; set; }
    }
}