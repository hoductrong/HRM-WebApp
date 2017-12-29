using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuanLyNongTrai.Helpers;
using QuanLyNongTrai.Model;
using QuanLyNongTrai.Repository;
using QuanLyNongTrai.Service;

namespace QuanLyNongTrai
{
    public partial class Startup
    {
        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            //configure repository
            services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IPersonalRepository, PersonalRepository>();
            services.AddTransient<IFamerRepository, FamerRepository>();
            //configure service
            services.AddTransient(typeof(IService<>), typeof(BaseService<>));
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IPersonalService, PersonalService>();
            services.AddTransient<IFamerService, FamerService>();
            //configure UnitOfWork
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //configure DbContext
            services.AddScoped<DbContext>(service => {
                return service.GetService<ApplicationDbContext>();
            });
        }
    }
}