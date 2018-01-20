using System;
using System.ComponentModel.DataAnnotations;
using QuanLyNongTrai.Model.Entity;

namespace QuanLyNongTrai.UI.Entity
{
    public class AccountModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Create UI model from Account entity
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static AccountModel CreateModel(ApplicationUser user)
        {
            if (user == null)
                return null;
            AccountModel model = new AccountModel{
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
            return model;
        }
    }
}