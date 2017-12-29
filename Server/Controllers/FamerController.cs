using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyNongTrai.Service;
using QuanLyNongTrai.UI.Entity;

namespace QuanLyNongTrai
{
    [Route("api/famers")]
    public class FamerController : Controller
    {
        private readonly IFamerService _famerService;
        public FamerController(
            IFamerService famerService)
        {
            _famerService = famerService;
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
                message = ResponseMessageModel.CreateResponse(
                    MessageCode.DATA_VALIDATE_ERROR);
                return message;
            }
            try
            {
                //add famer to database
                var result = _famerService.Add(famer);
                if (!result.Succeeded)
                {
                    message = ResponseMessageModel.CreateResponse(
                        MessageCode.DATA_VALIDATE_ERROR,
                        result.GetError()
                    );
                    return message;
                }
                //add success
                return FamerModel.GetModel(famer);
            }
            catch (SqlException ex)
            {
                //error
                message = ResponseMessageModel.CreateResponse(MessageCode.SQL_ACTION_ERROR, ex.Message);
                return message;
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
                var famers = _famerService.GetAllFamerDetail();
                //create FamerModel
                List<FamerModel> models = new List<FamerModel>();
                foreach (var famer in famers)
                {
                    var model = FamerModel.GetModel(famer);
                    models.Add(model);
                }
                return this.Message(models);
            }
            catch (SqlException ex)
            {
                return this.Message(MessageCode.SQL_ACTION_ERROR, ex.Message);
            }
        }
        [Route("{famerId}")]
        [HttpPut]
        [Authorize(Roles = "manager,famer")]
        public object UpdateFamer(Guid famerId,[ModelBinder]FamerModel model)
        {
            Guid id = model.FamerId;
            return null;
        }
    }
}