using Microsoft.AspNetCore.Mvc;
using System;
using QuanLyNongTrai.Service;
using QuanLyNongTrai.Helpers;
using QuanLyNongTrai.UI.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QuanLyNongTrai.Model.Entity;
using System.Data.SqlClient;

namespace QuanLyNongTrai
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ApplicationUserManager _userManager;

        public EmployeeController(IEmployeeService employeeService,
                ApplicationUserManager userManager)
        {
            _employeeService = employeeService;
            _userManager = userManager;
        }

        [Route("")]
        [HttpPost]
        [Authorize(Roles = "humanresources")]
        public object Add([FromBody]EmployeeModel model)
        {
            ResponseMessageModel message;
            if (!ModelState.IsValid)
            {
                return this.Message(MessageCode.DATA_VALIDATE_ERROR,this.GetError(ModelState.Values));
            }

            Employee employee = new Employee
            {
                Salary = model.Salary,
                StartWorkTime = model.StartWorkTime,
                EndWorkTime = model.EndWorkTime,
                Personal = new Personal
                {
                    FullName = model.FullName,
                    Address = model.Address,
                    Sex = model.Sex == (int)SexEnum.Male ? true : false,
                    BirthDay = model.BirthDay,
                    Phone = model.Phone,
                    Description = model.Description,
                }
            };
            try
            {
                _employeeService.Add(employee);

                //parse response
                model.EmployeeId = employee.Id;
                model.PersonalId = employee.Personal.Id;
                return this.Message(model);
            }
            catch (Exception ex)
            {
                message = this.Message(MessageCode.SQL_ACTION_ERROR,ex.Message);
                return message;
            }
        }

        [Route("")]
        [HttpGet]
        [Authorize(Roles = "humanresources")]
        public object GetAllEmployee()
        {
            ResponseMessageModel message;
            try
            {
                message = this.Message(_employeeService.GetAllEmployeeDetail());
                return message;
            }
            catch (Exception ex)
            {
                message = this.Message(MessageCode.APPLICATION_ERROR,ex.Message);
                return message;
            }
        }

        [Route("{employeeId}")]
        [HttpDelete]
        [Authorize(Roles = "humanresources")]
        public object Delete(Guid employeeId)
        {
            ResponseMessageModel message;
            var employee = _employeeService.Find(employeeId);
            //entity isn't found
            if (employee == null)
            {
                message = this.Message(MessageCode.OBJECT_NOT_FOUND);
                return message;
            }
            //Entity is Found,delete entity
            try
            {
                var result = _employeeService.Delete(employee);
                if (result.Succeeded)
                {
                    message = this.Message(null);
                    return message;
                }
                else
                {
                    message = this.Message(
                        MessageCode.DATA_VALIDATE_ERROR,
                        result.GetError());
                    return message;
                }
            }
            catch (SqlException ex)
            {
                message = this.Message(MessageCode.SQL_ACTION_ERROR, ex.Message);
                return message;
            }
        }

        /// <summary>
        /// Get an employee' information
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <returns></returns>
        [Route("{employeeId}")]
        [HttpGet]
        [Authorize(Roles = "humanresources")]
        public object GetEmployee(Guid employeeId)
        {
            ResponseMessageModel message;
            var employee = _employeeService.Find(employeeId);
            //entity isn't found
            if (employee == null)
            {
                message = this.Message(MessageCode.OBJECT_NOT_FOUND);
                return message;
            }
            //found
            EmployeeModel model = EmployeeModel.GetModel(employee);
            message = this.Message(model);
            return message;
        }

        /// <summary>
        /// Update employee information
        /// </summary>
        /// <param name="employeeId">Id of employee</param>
        /// <param name="model">employee information</param>
        /// <returns>employee information is updated</returns>
        [Route("{employeeId}")]
        [HttpPut]
        [Authorize(Roles = "humanresources")]
        public object EditEmployee(Guid employeeId,[FromBody] EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = model.GetEntity();
                employee.Id = employeeId;
                //if it is found, then update 
                var result = _employeeService.Update(employee);
                //update success
                if (result.Succeeded)
                {
                    return this.Message(MessageCode.SUCCESS);
                }
                //update failed
                return this.Message(MessageCode.SQL_ACTION_ERROR, result.GetError());
            }
            else
            {
                //get and return error
                string errors = this.GetError(ModelState.Values);
                return this.Message(MessageCode.DATA_VALIDATE_ERROR, errors);
            }
        }

        /// <summary>
        /// Get Roles of account that employee own
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>List role name</returns>
        [Route("{employeeId}/account")]
        [HttpGet]
        [Authorize(Roles="humanresources")]
        public object GetRoles(Guid employeeId){
            if(employeeId == Guid.Empty)
                return ResponseMessageModel.CreateResponse(MessageCode.PARAMETER_NULL);
            //find personal id by employeeId
            var employee = _employeeService.Find(employeeId);
            if(employee == null)
                return ResponseMessageModel.CreateResponse(MessageCode.OBJECT_NOT_FOUND);
            //get role of account by personalId
            var result = _userManager.GetAccountByPersonalId(employee.PersonalId);
            return ResponseMessageModel.CreateResponse(result);
        }
    }
}