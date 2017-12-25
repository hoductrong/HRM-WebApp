using System;

namespace QuanLyNongTrai.UI.Entity
{
    public class UserRegisterModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public Guid PersonalId { get; set; }
    }
}