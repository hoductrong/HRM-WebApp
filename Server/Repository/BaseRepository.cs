using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai.Repository{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        public BaseRepository(DbContext context){
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public virtual void Add(TEntity entity)
        {
            _dbSet.AddRange(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if(_dbContext.Entry(entity).State == EntityState.Deleted){
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual TEntity Find(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}