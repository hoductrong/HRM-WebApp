using System;
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
        IEnumerable<FamerModel> GetAllFamerDetail();
        /// <summary>
        /// Update Famer information
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ChangeDataResult UpdateFamerWithPersonal(Famer entity);
        /// <summary>
        /// Get personal information of famer
        /// </summary>
        /// <param name="famerId">Id of famer</param>
        /// <returns></returns>
        Personal GetPersonal(Guid famerId);
    }
}