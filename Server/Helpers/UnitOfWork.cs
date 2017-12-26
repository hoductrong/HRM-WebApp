using System;
using System.Data.SqlClient;
using DependencyInjectionSample.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QuanLyNongTrai.Model;
using QuanLyNongTrai.Model.Entity;
using QuanLyNongTrai.Repository;

namespace QuanLyNongTrai.Helpers
{
    public class UnitOfWork : IUnitOfWork
    {
        private static DbContext _dbContext = null;
        private IDbContextTransaction _transaction;

        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
        }

        public void SaveChanges()
        {
            if(_transaction == null)
                return;
            _transaction.Commit();
            _dbContext.SaveChanges();
        }

        public void RollBack()
        {
            if(_transaction != null)
                _transaction.Rollback();
        }

        public void Dispose()
        {
            if(_transaction != null)
                _transaction.Dispose();
        }

        public void BeginTransaction()
        {
            if (_dbContext.Database.CurrentTransaction == null)
                _transaction = _dbContext.Database.BeginTransaction();
            else
                _transaction = _dbContext.Database.CurrentTransaction;
        }
    }
}