using System;
using System.ComponentModel.DataAnnotations;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai.UI.Entity
{
    public class EmployeeModel
    {
        public Guid? EmployeeId { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public DateTime StartWorkTime { get; set; }
        public DateTime? EndWorkTime { get; set; }
        public Guid? PersonalId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [EnumDataType(typeof(SexEnum))]
        public int Sex { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }

        public static EmployeeModel GetModel(Employee employee){
            if(employee == null || employee.Personal == null)
                return null;
            return new EmployeeModel{
                EmployeeId = employee.Id,
                Salary = employee.Salary,
                StartWorkTime = employee.StartWorkTime,
                EndWorkTime = employee.EndWorkTime,
                PersonalId = employee.Personal.Id,
                FullName = employee.Personal.FullName,
                Address = employee.Personal.Address,
                Sex = employee.Personal.Sex == true ? 1 : 0,
                BirthDay = employee.Personal.BirthDay,
                Phone = employee.Personal.Phone,
                Description = employee.Personal.Description
            };
        }
    }
}