using System.Collections.Generic;
using QuanLyNongTrai.Model.Entity;
using QuanLyNongTrai.UI.Entity;

namespace QuanLyNongTrai.Service
{
    public interface IEmployeeService : IService<Employee> 
    {
        /// <summary>
        /// Get all employee with personal information
        /// </summary>
        /// <returns>Employee list</returns>
        IEnumerable<EmployeeModel> GetAllEmployeeDetail();
    }
}