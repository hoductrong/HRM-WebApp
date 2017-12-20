using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNongTrai.Model.Entity
{
    [Table("Employees")]
    public class Employee : BaseEntity
    {
        public decimal Salary { get; set; }
        public DateTime StartWorkTime { get; set; }
        public DateTime? EndWorkTime { get; set; }
        //foreign key id
        public Guid PersonalId { get; set; }
        //relationship
        public Personal Personal { get; set; }
    }
}