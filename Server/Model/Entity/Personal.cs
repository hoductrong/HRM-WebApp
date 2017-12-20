using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNongTrai.Model.Entity
{
    [Table("Personal")]
    public class Personal : BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string FullName { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        public bool Sex { get; set; }
        public DateTime? BirthDay { get; set; }
        [MaxLength(11)]
        [Required]
        public string Phone { get; set; }
        //relationship
        public Famer Famer { get; set; }
        public Employee Employee { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}