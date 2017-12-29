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
    }
}