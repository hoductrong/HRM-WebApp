using System;
using System.Collections.Generic;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai.UI.Entity
{
    public class UserInformationModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public static UserInformationModel GetModel(ApplicationUser entity){
            UserInformationModel model = new UserInformationModel{
                Id = entity.Id,
                Email = entity.Email,
                UserName = entity.UserName
            };
            return model;
        }
    }
}