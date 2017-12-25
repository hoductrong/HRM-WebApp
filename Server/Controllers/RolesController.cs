using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyNongTrai.Model.Entity;
using System;

namespace QuanLyNongTrai
{
    [Route("api/roles")]
    [Authorize]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RolesController(RoleManager<ApplicationRole> roleManager){
            _roleManager = roleManager;
        }

        [HttpPost]
        [Authorize(Roles= "manager")]
        public async Task<object> CreateNewRole([FromBody]ApplicationRole model){
            if(model.Name.Length <= 0)
                throw new ArgumentNullException("RoleName is null");
            var role = new ApplicationRole{
                Id = Guid.NewGuid(),
                Name = model.Name
            };
            var result = await _roleManager.CreateAsync(role);
            if(result.Succeeded)
                return role;
            throw new ApplicationException("ERROR_ADD_NEW_ROLE");
        }
    }
}