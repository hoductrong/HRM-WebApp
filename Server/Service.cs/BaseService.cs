using System;
using System.Collections.Generic;
using DependencyInjectionSample.Interfaces;
using QuanLyNongTrai.Model.Entity;
using QuanLyNongTrai.Repository;

namespace QuanLyNongTrai.Service
{
    public class BaseService<TEntity> : IService<TEntity> where TEntity : BaseEntity
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            _repository.Add(entity);
            _unitOfWork.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException();
            entity.IsDelete = true;
            _repository.Update(entity);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public virtual TEntity Find(object id)
        {
            return _repository.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            _repository.Update(entity);
        }

        public virtual bool Validate(TEntity entity)
        {
            return true;
        }
    }
}
