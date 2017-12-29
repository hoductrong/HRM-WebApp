using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyNongTrai.Helpers;
using QuanLyNongTrai.Model.Entity;
using QuanLyNongTrai.Repository;
using QuanLyNongTrai.UI.Entity;

namespace QuanLyNongTrai.Service
{
    public class FamerService : BaseService<Famer>, IFamerService
    {
        private readonly IFamerRepository _famerRepository;
        private readonly IPersonalService _personalService;
        public FamerService(
            IUnitOfWork unitOfWork,
            IFamerRepository famerRepository,
            IPersonalService personalService) : base(unitOfWork, famerRepository)
        {
            _famerRepository = famerRepository;
            _personalService = personalService;
        }

        /// <summary>
        /// Add new famer
        /// </summary>
        /// <param name="entity">Famer infomation</param>
        /// <returns></returns>
        public override ChangeDataResult Add(Famer entity)
        {
            ChangeDataResult result;
            if (entity.Personal == null)
            {
                result = ChangeDataResult.Fails(new ChangeDataError
                {
                    Code = MessageCode.DATA_VALIDATE_ERROR,
                    Description = "Phải điền đầy đủ thông tin về nông dân"
                });
                return result;
            }
            result = Validate(entity);
            //validate entity faild
            if (!result.Succeeded)
            {
                return result;
            }
            _unitOfWork.BeginTransaction();
            try
            {
                //add personal
                result = _personalService.Add(entity.Personal);
                //add personal fail
                if (!result.Succeeded)
                    return result;
                //add personal success
                //then add employee
                entity.Id = Guid.NewGuid();
                _famerRepository.Add(entity);
                _unitOfWork.Commit();
                _unitOfWork.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return ChangeDataResult.Fails(new ChangeDataError
                {
                    Code = MessageCode.SQL_ACTION_ERROR,
                    Description = ex.Message
                });
            }
        }

        public IEnumerable<Famer> GetAllFamerDetail()
        {
            var famers = base.GetAll();
            foreach (var famer in famers)
            {
                famer.Personal = _personalService.Find(famer.PersonalId);
            }
            return famers;
        }
        /// <summary>
        /// Get personal information of famer
        /// </summary>
        /// <param name="famerId">Id of famer</param>
        /// <returns></returns>
        public Personal GetPersonal(Guid famerId)
        {
            var famer = _famerRepository.Find(famerId);
            return _personalService.Find(famer.PersonalId);
        }

        /// <summary>
        /// Update Famer information
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ChangeDataResult UpdateFamerWithPersonal(Famer entity)
        {
            if (entity.Personal == null)
                return ChangeDataResult.Fails(new ChangeDataError
                {
                    Code = MessageCode.DATA_VALIDATE_ERROR
                });
            var result = Validate(entity);
            //validate faild
            if (!result.Succeeded)
            {
                return ChangeDataResult.Fails(new ChangeDataError
                {
                    Code = MessageCode.DATA_VALIDATE_ERROR
                });
            }
            //validate success
            _unitOfWork.BeginTransaction();
            try
            {
                //update personal information
                result = _personalService.Update(entity.Personal);
                if (!result.Succeeded)
                {
                    return ChangeDataResult.Fails(new ChangeDataError
                    {
                        Code = MessageCode.SQL_ACTION_ERROR,
                        Description = result.GetError()
                    });
                }
                //update personal success
                //then update employee
                entity.Personal = null;
                _famerRepository.Update(entity);
                _unitOfWork.Commit();
                _unitOfWork.SaveChanges();
                return new ChangeDataResult();
            }
            catch (SqlException ex)
            {
                //error, rollback change
                _unitOfWork.RollBack();
                return ChangeDataResult.Fails(new ChangeDataError
                {
                    Code = MessageCode.SQL_ACTION_ERROR,
                    Description = ex.Message
                });
            }
        }
    }
}