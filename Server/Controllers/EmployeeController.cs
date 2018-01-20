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
        [Authorize(Roles = "humanresouces")]
        public object Add([FromBody]EmployeeModel model)
        {
            ResponseMessageModel message;
            if (model == null)
            {
                message = new ResponseMessageModel
                {
                    Code = MessageCode.PARAMETER_NULL,
                    ErrorMessage = "Parameter is null"
                };
                return message;
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
                return new ResponseMessageModel
                {
                    Code = MessageCode.SUCCESS,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                message = new ResponseMessageModel
                {
                    Code = MessageCode.SQL_ACTION_ERROR,
                    ErrorMessage = ex.Message
                };
                return message;
            }
        }

        [Route("")]
        [HttpGet]
        [Authorize(Roles = "humanresouces")]
        public object GetAllEmployee()
        {
            ResponseMessageModel message;
            try
            {
                message = new ResponseMessageModel
                {
                    Code = MessageCode.SUCCESS,
                    Data = _employeeService.GetAllEmployeeDetail()
                };
                return message;
            }
            catch (Exception ex)
            {
                message = new ResponseMessageModel
                {
                    Code = MessageCode.APPLICATION_ERROR,
                    ErrorMessage = ex.Message
                };
                return message;
            }
        }

        [Route("{employeeId}")]
        [HttpDelete]
        [Authorize(Roles = "humanresouces")]
        public object Delete(Guid employeeId)
        {
            ResponseMessageModel message;
            var employee = _employeeService.Find(employeeId);
            //entity isn't found
            if (employee == null)
            {
                message = ResponseMessageModel.CreateResponse(MessageCode.OBJECT_NOT_FOUND);
                return message;
            }
            //Entity is Found,delete entity
            try
            {
                var result = _employeeService.Delete(employee);
                if (result.Succeeded)
                {
                    message = ResponseMessageModel.CreateResponse(null);
                    return message;
                }
                else
                {
                    message = ResponseMessageModel.CreateResponse(
                        MessageCode.DATA_VALIDATE_ERROR,
                        result.GetError());
                    return message;
                }
            }
            catch (SqlException ex)
            {
                message = ResponseMessageModel.CreateResponse(MessageCode.SQL_ACTION_ERROR, ex.Message);
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
        [Authorize(Roles = "humanresouces")]
        public object GetEmployee(Guid employeeId)
        {
            ResponseMessageModel message;
            var employee = _employeeService.Find(employeeId);
            //entity isn't found
            if (employee == null)
            {
                message = ResponseMessageModel.CreateResponse(MessageCode.OBJECT_NOT_FOUND);
                return message;
            }
            //found
            EmployeeModel model = EmployeeModel.GetModel(employee);
            message = ResponseMessageModel.CreateResponse(model);
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
        [Authorize(Roles = "humanresouces")]
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
                    return ResponseMessageModel.CreateResponse(MessageCode.SUCCESS);
                }
                //update failed
                return ResponseMessageModel.CreateResponse(MessageCode.SQL_ACTION_ERROR, result.GetError());
            }
            else
            {
                //get and return error
                string errors = this.GetError(ModelState.Values);
                return ResponseMessageModel.CreateResponse(MessageCode.DATA_VALIDATE_ERROR, errors);
            }
        }

        /// <summary>
        /// Get Roles of account that employee own
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>List role name</returns>
        [Route("{employeeId}/account")]
        [HttpGet]
        [Authorize(Roles="humanresouces")]
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