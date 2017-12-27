using System;
using DependencyInjectionSample.Interfaces;
using QuanLyNongTrai.Model.Entity;
using QuanLyNongTrai.Repository;
using QuanLyNongTrai.UI.Entity;

namespace QuanLyNongTrai.Service
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        private IPersonalService _personalService;

        public EmployeeService(
            IUnitOfWork unitOfWork,
            IEmployeeRepository employeeRepository,
            IPersonalService personalService)
            : base(unitOfWork, employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _personalService = personalService;
        }

        public override ChangeDataResult Add(Employee entity)
        {
            ChangeDataResult result;
            _unitOfWork.BeginTransaction();
            try
            {
                if (entity == null)
                    throw new ArgumentNullException();
                Personal personal = entity.Personal;
                result = _personalService.Add(personal);
                if (!result.Succeeded)
                {
                    return result;
                }
                result = Validate(entity);
                if (!result.Succeeded)
                {
                    return result;
                }
                entity.Id = Guid.NewGuid();
                _employeeRepository.Add(entity);
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
    }
}