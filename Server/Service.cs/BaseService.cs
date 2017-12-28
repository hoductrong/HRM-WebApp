using System;
using System.Collections.Generic;
using System.Linq;
using DependencyInjectionSample.Interfaces;
using QuanLyNongTrai.Model.Entity;
using QuanLyNongTrai.Repository;
using QuanLyNongTrai.UI.Entity;

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

        public virtual ChangeDataResult Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            var result = Validate(entity);
            if (!result.Succeeded)
                return result;
            _repository.Add(entity);
            _unitOfWork.SaveChanges();
            return result;
        }

        public virtual ChangeDataResult Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException();
            entity.IsDelete = true;
            _repository.Update(entity);
            _unitOfWork.SaveChanges();
            return new ChangeDataResult();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public virtual TEntity Find(object id)
        {
            var entity =  _repository.Find(id);
            if(entity != null && entity.IsDelete == true)
                return null;
            return entity;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll().Where(e => e.IsDelete == false);
        }

        public virtual ChangeDataResult Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            var result = Validate(entity);
            if (!result.Succeeded)
                return result;
            _repository.Update(entity);
            _unitOfWork.SaveChanges();
            return result;
        }

        public virtual ChangeDataResult Validate(TEntity entity)
        {
            return new ChangeDataResult();
        }
    }
}
