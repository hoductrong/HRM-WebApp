using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyNongTrai.Model.Entity;
using QuanLyNongTrai.Service;
using QuanLyNongTrai.UI.Entity;

namespace QuanLyNongTrai
{
    [Route("api/famers")]
    public class FamerController : Controller
    {
        private readonly IFamerService _famerService;
        private ApplicationUserManager _userManager;
        public FamerController(
            IFamerService famerService,
            IPersonalService personalService,
            ApplicationUserManager userManager)
        {
            _famerService = famerService;
            _userManager = userManager;
        }

        /// <summary>
        /// Add new Famer
        /// </summary>
        /// <returns>Famer information added</returns>
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "manager")]
        public object AddFamer([FromBody]FamerModel model)
        {
            ResponseMessageModel message;
            //change model to Famer object
            var famer = model.CreateEntity();
            if (famer == null)
            {
                return this.Message(MessageCode.DATA_VALIDATE_ERROR);
            }
            try
            {
                //add famer to database
                var result = _famerService.Add(famer);
                if (!result.Succeeded)
                {
                    return this.Message(MessageCode.DATA_VALIDATE_ERROR,result.GetError());
                }
                //add success
                return this.Message(FamerModel.GetModel(famer));
            }
            catch (SqlException ex)
            {
                return this.Message(MessageCode.SQL_ACTION_ERROR,ex.Message);
            }
        }

        [Route("{famerId}")]
        [HttpDelete]
        [Authorize(Roles = "manager")]
        public object DeleteFamer(Guid famerId)
        {
            var famer = _famerService.Find(famerId);
            //user isn't found
            if (famer == null)
            {
                return this.Message(MessageCode.OBJECT_NOT_FOUND);
            }
            //found user
            //then delete user
            try
            {
                _famerService.Delete(famer);
                return this.Message(MessageCode.SUCCESS);
            }
            catch (SqlException ex)
            {
                //error
                return this.Message(MessageCode.SQL_ACTION_ERROR, ex.Message);
            }
        }

        [Route("")]
        [HttpGet]
        [Authorize(Roles = "manager")]
        public object GetAllFamer()
        {
            try
            {
                var famersModel = _famerService.GetAllFamerDetail();
                return this.Message(famersModel);
            }
            catch (SqlException ex)
            {
                return this.Message(MessageCode.SQL_ACTION_ERROR, ex.Message);
            }
        }
        [Route("{famerId}")]
        [HttpPut]
        [Authorize(Roles = "manager,famer")]
        public object UpdateFamer(Guid famerId, [FromBody]FamerModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.PersonalId == Guid.Empty){
                    return this.Message(MessageCode.DATA_VALIDATE_ERROR,"Yêu cầu PersonalId");
                }
                var famer = model.CreateEntity();
                famer.Id = famerId;
                //update model
                try
                {
                    var result = _famerService.UpdateFamerWithPersonal(famer);
                    if (result.Succeeded)
                    {
                        return this.Message(FamerModel.GetModel(famer));
                    }else{
                        //error
                        return this.Message(MessageCode.DATA_VALIDATE_ERROR,result.GetError());
                    }
                }
                catch (SqlException ex)
                {
                    return this.Message(MessageCode.SQL_ACTION_ERROR, ex.Message);
                }
            }
            else
            {
                return this.Message(MessageCode.DATA_VALIDATE_ERROR);
            }
        }

        /// <summary>
        /// Get Roles of account that famer own
        /// </summary>
        /// <param name="famerId"></param>
        /// <returns>List role name</returns>
        [Route("{employeeId}/account")]
        [HttpGet]
        [Authorize(Roles="humanresouces")]
        public object GetRoles(Guid famerId){
            if(famerId == Guid.Empty)
                return ResponseMessageModel.CreateResponse(MessageCode.PARAMETER_NULL);
            //find personal id by famerId
            var employee = _famerService.Find(famerId);
            if(employee == null)
                return ResponseMessageModel.CreateResponse(MessageCode.OBJECT_NOT_FOUND);
            //get role of account by personalId
            var result = _userManager.GetAccountByPersonalId(employee.PersonalId);
            return ResponseMessageModel.CreateResponse(result);
        }
    }
}