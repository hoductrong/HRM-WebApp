using System.Collections.Generic;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai.Repository{
    public interface IRepository<TEntity> where TEntity : BaseEntity{
        /// <summary>
        /// Get all record
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();
        /// <summary>
        /// Get all record and includes table of it
        /// </summary>
        /// <param name="includes">Name of properties need include</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(params string[] includes);

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
        /// Find a entity with Id. Include property
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includes">Name of properties</param>
        /// <returns></returns>
        TEntity Find(object id, params string[] includes);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
    }
}