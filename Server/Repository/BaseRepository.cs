using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        public BaseRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public virtual void Add(TEntity entity)
        {
            _dbSet.AddRange(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Deleted)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual TEntity Find(object id)
        {
            return _dbSet.Find(id);
        }
        /// <summary>
        /// Find a entity with Id. Include property
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includes">Name of properties</param>
        /// <returns></returns>
        public TEntity Find(object id, params string[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            query = query.Where(e => e.Id == (Guid)id);
            foreach (var property in includes)
            {
                query = query.Include(property);
            }
            return query.SingleOrDefault();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet;
        }

        /// <summary>
        /// Get all record and includes table of it
        /// </summary>
        /// <param name="includes">Name of properties need include</param>
        /// <returns></returns>

        public IEnumerable<TEntity> GetAll(params string[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            foreach (var property in includes)
            {
                query = query.Include(property);
            }
            return query.ToList();
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}