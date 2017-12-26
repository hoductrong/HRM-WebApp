using Microsoft.EntityFrameworkCore;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai.Repository
{
    public class PersonalRepository : BaseRepository<Personal>, IPersonalRepository
    {
        public PersonalRepository(DbContext context) : base(context)
        {
        }
    }
}