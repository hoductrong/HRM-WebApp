using System.Collections.Generic;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai.Service
{
    public interface IPersonalService : IService<Personal>
    {
        /// <summary>
        /// Get personal information and owner account
        /// </summary>
        /// <returns>Personal list</returns>
        IEnumerable<Personal> GetPersonalWithAccount();
    }
}