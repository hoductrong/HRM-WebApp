using System;
using System.Collections.Generic;
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
            try
            {
                if (entity == null)
                    throw new ArgumentNullException();
                var result = Validate(entity);
                if (!result.Succeeded)
                    return result;
                _repository.Add(entity);
                return result;
            }
            catch (Exception ex)
            {
                return ChangeDataResult.Fails(
                    new ChangeDataError
                    {
                        Code = MessageCode.SQL_ACTION_ERROR,
                        Description = ex.Message
                    });
            }
        }

        public virtual ChangeDataResult Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentException();
                entity.IsDelete = true;
                _repository.Update(entity);
                return new ChangeDataResult();
            }
            catch (Exception ex)
            {
                return ChangeDataResult.Fails(new ChangeDataError
                {
                    Code = MessageCode.SQL_ACTION_ERROR,
                    Description = ex.Message
                });
            }
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

        public virtual ChangeDataResult Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException();
                var result = Validate(entity);
                if (!result.Succeeded)
                    return result;
                _repository.Update(entity);
                return result;

            }
            catch (Exception ex)
            {
                return ChangeDataResult.Fails(new ChangeDataError
                {
                    Code = MessageCode.SQL_ACTION_ERROR,
                    Description = ex.Message
                });
            }
        }

        public virtual ChangeDataResult Validate(TEntity entity)
        {
            return new ChangeDataResult();
        }
    }
}
