using Microsoft.AspNetCore.Mvc;
using System;
using DependencyInjectionSample.Interfaces;
using QuanLyNongTrai.Service;
using QuanLyNongTrai.UI.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai
{
    [Route("api/employees")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Route("")]
        [HttpPost]
        [Authorize(Roles = "manager")]
        public object Add([FromBody]EmployeeModel model)
        {
            ResponseMessageModel message;
            if (model == null)
            {
                message = new ResponseMessageModel {
                    Code = MessageCode.PARAMETER_NULL,
                    ErrorMessage = "Parameter is null"
                };
                return message;
            }

            Employee employee = new Employee {
                Salary = model.Salary,
                StartWorkTime = model.StartWorkTime,
                EndWorkTime = model.EndWorkTime,
                Personal = new Personal{
                    FullName = model.FullName,
                    Address = model.Address,
                    Sex = model.Sex == (int)SexEnum.Male ? true : false,
                    BirthDay = model.BirthDay,
                    Phone = model.Phone,
                    Description = model.Description,
                }
            };
            try{
                _employeeService.Add(employee);

                //parse response
                model.EmployeeId = employee.Id;
                model.PersonalId = employee.Personal.Id;
                return new ResponseMessageModel{
                    Code = MessageCode.SUCCESS,
                    Data = model
                };
            }catch(Exception ex){
                message = new ResponseMessageModel{
                    Code = MessageCode.SQL_ACTION_ERROR,
                    ErrorMessage = ex.Message
                };
                return message;
            }
        }

        [Route("")]
        [HttpGet]
        [Authorize(Roles = "manager")]
        public object GetAllEmployee(){
            ResponseMessageModel message;
            try{
                message = new ResponseMessageModel {
                    Code = MessageCode.SUCCESS,
                    Data = _employeeService.GetAll()
                };
                return message;
            }catch(Exception ex){
                message = new ResponseMessageModel{
                    Code = MessageCode.APPLICATION_ERROR,
                    ErrorMessage = ex.Message
                };
                return message;
            }
        }
    }
}