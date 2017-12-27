using System;
using System.Collections.Generic;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai.Service{
    public interface IService<TEntity> : IDisposable where TEntity : BaseEntity{
        /// <summary>
        /// Get all record
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Add new entity
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);
        
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Find a entity with Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        TEntity Find(object id);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Check business logic
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Validate(TEntity entity);
    }
}