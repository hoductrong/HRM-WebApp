using System;
using DependencyInjectionSample.Interfaces;
using QuanLyNongTrai.Model.Entity;
using QuanLyNongTrai.Repository;

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

        public override void Add(Employee entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            _unitOfWork.BeginTransaction();
            try
            {
                Personal personal = entity.Personal;
                _personalService.Add(personal);
                entity.Id = Guid.NewGuid();
                _employeeRepository.Add(entity);
                _unitOfWork.SaveChanges();
            }catch(Exception ex){
                _unitOfWork.RollBack();
                throw ex;
            }
        }
    }
}