using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNongTrai.UI.Entity
{
    public class EmployeeModel
    {
        public Guid EmployeeId { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public DateTime StartWorkTime { get; set; }
        public DateTime EndWorkTime { get; set; }
        public Guid PersonalId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [EnumDataType(typeof(SexEnum))]
        public int Sex { get; set; }
        public DateTime BirthDay { get; set; }
        public string Phone { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
    }
}