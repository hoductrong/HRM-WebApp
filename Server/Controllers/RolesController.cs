using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyNongTrai.Model.Entity;
using System;
using QuanLyNongTrai.UI.Entity;

namespace QuanLyNongTrai
{
    [Route("api/roles")]
    [Authorize]
    public class RolesController : Controller
    {
        private readonly ApplicationRoleManager _roleManager;
        public RolesController(ApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<object> CreateNewRole([FromBody]ApplicationRole model)
        {   ResponseMessageModel message;
            var role = new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = model.Name
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                message = new ResponseMessageModel{
                    Code = MessageCode.SUCCESS,
                    Data = role
                };
                return message;
            }
            //error occurs
            string errorMessage = "";
            foreach(var e in result.Errors){
                errorMessage += e.Description + "\r\n";
            }
            message = new ResponseMessageModel{
                Code = MessageCode.SQL_ACTION_ERROR,
                ErrorMessage = errorMessage
            };
            return message;
        }

        [Route("{roleId}")]
        [HttpDelete]
        [Authorize(Roles = "manager")]
        public async Task<object> DeleteRole(Guid roleId)
        {
            ResponseMessageModel message;
            if (roleId == null)
            {
                message = new ResponseMessageModel{
                    Code = MessageCode.PARAMETER_NULL
                };
                return message;
            }
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                message = new ResponseMessageModel{
                    Code = MessageCode.OBJECT_NOT_FOUND
                };
                return message;
            }
            var result = await _roleManager.DeleteAsync(role);
            if(result.Succeeded){
                message = new ResponseMessageModel{
                    Code = MessageCode.SUCCESS,
                };
                return message;
            }
            //error occurs
            message = new ResponseMessageModel{
                Code = MessageCode.SQL_ACTION_ERROR,
                ErrorMessage = result.ToString()
            };
            return message;
        }
    }
}