using Microsoft.EntityFrameworkCore;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai.Repository{

    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context) : base(context)
        {
            
        }
    }
}