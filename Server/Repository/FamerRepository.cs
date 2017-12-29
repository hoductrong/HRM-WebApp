using Microsoft.EntityFrameworkCore;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai.Repository
{
    public class FamerRepository : BaseRepository<Famer>, IFamerRepository
    {
        public FamerRepository(DbContext context) : base(context)
        {
        }
    }
}