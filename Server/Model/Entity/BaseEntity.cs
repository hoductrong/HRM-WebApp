using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNongTrai.Model.Entity
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        public bool IsDelete { get; set; }
    }
}