using System.Collections.Generic;
using QuanLyNongTrai.Model.Entity;
using QuanLyNongTrai.UI.Entity;

namespace QuanLyNongTrai.Service
{
    public interface IFamerService : IService<Famer>
    {
        /// <summary>
        /// Get All famer detail.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Famer> GetAllFamerDetail();
    }
}