using System.Collections.Generic;
using QuanLyNongTrai.Helpers;
using QuanLyNongTrai.Model.Entity;
using QuanLyNongTrai.Repository;

namespace QuanLyNongTrai.Service
{
    public class PersonalService : BaseService<Personal>, IPersonalService
    {
        private readonly IPersonalRepository _personalRepository;
        public PersonalService(IUnitOfWork unitOfWork, IPersonalRepository personalRepository) 
            : base(unitOfWork, personalRepository)
        {
            _personalRepository = personalRepository;
        }

        /// <summary>
        /// Get personal information and owner account
        /// </summary>
        /// <returns>Personal list</returns>
        public IEnumerable<Personal> GetPersonalWithAccount()
        {
            return _personalRepository.GetAll("ApplicationUser");
        }
    }
}