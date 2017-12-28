using System;
using Microsoft.EntityFrameworkCore;
using QuanLyNongTrai.Model.Entity;
using QuanLyNongTrai.Repository;

namespace DependencyInjectionSample.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Begin traction
        /// </summary>
        void BeginTransaction();
        
        /// <summary>
        /// Save change to database
        /// </summary>
        void SaveChanges();
        /// <summary>
        /// Commit transaction
        /// </summary>
        void Commit();

        /// <summary>
        /// Revert changes
        /// </summary>
        void RollBack();
    }
}