using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using QuanLyNongTrai.Helpers;
using QuanLyNongTrai.Model;
using QuanLyNongTrai.Model.Entity;
using QuanLyNongTrai.Service;
using QuanLyNongTrai.UI.Entity;

namespace QuanLyNongTrai
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IPersonalService _personalService;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IPersonalService personalService,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _personalService = personalService;
            _configuration = configuration;
        }

        [Route("token")]
        [HttpPost]
        public async Task<object> Login([FromBody]UserLoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            
            ResponseMessageModel message;
            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.UserName);
                //require change password
                if (appUser.PasswordChanged == false)
                {
                    message = new ResponseMessageModel
                    {
                        Code = MessageCode.ERROR,
                        ErrorMessage = "Bạn phải thay đổi mật khẩu cho lần đầu đăng nhập"
                    };
                    return StatusCode(StatusCodes.Status403Forbidden,message);
                }
                var token = new TokenModel
                {
                    Token = (string)(await GenerateJwtToken(model.UserName, appUser))
                };
                return new ResponseMessageModel {
                    Code = MessageCode.SUCCESS,
                    Data = token
                };
            }
            message = new ResponseMessageModel{
                Code = MessageCode.ERROR,
                ErrorMessage = "Tài khoản hoặc mật khẩu không chính xác"
            };
            return StatusCode(StatusCodes.Status403Forbidden,message);
            
        }

        [Route("")]
        [HttpPut]
        public async Task<object> ChangePassword([FromBody]UserChangePasswordModel model)
        {
            ResponseMessageModel message;
            if (model == null)
            {
                message = new ResponseMessageModel{
                    Code = MessageCode.PARAMETER_NULL
                };
                return message;
            }


            if (model.Password != model.RePassword)
            {
                message = new ResponseMessageModel
                {
                    Code = MessageCode.DATA_VALIDATE_ERROR,
                    ErrorMessage = "Mật khẩu không khớp nhau"
                };
                return message;
            }

            ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
            if(user == null){
                message = new ResponseMessageModel
                {
                    Code = MessageCode.OBJECT_NOT_FOUND,
                    ErrorMessage = "Không tìm thấy user"
                };
                return message;
            }

            var result = await _userManager.ChangePasswordAsync(user,model.OldPassword, model.Password);
            if(result.Succeeded){
                user.PasswordChanged = true;
                await _userManager.UpdateAsync(user);
                var token = new TokenModel
                {
                    Token = (string)(await GenerateJwtToken(model.UserName, user))
                };
                return new ResponseMessageModel {
                    Code = MessageCode.SUCCESS,
                    Data = token
                };
            }
            
            message = new ResponseMessageModel{
                Code = MessageCode.SQL_ACTION_ERROR,
                ErrorMessage = "Có lỗi xảy ra, Không thể thay đổi mật khẩu"
            };
            return message;
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<object> Register([FromBody]UserRegisterModel userRegister)
        {
            ResponseMessageModel message;
            if (userRegister.Password == null || userRegister.Password.Length <= 0)
            {
                userRegister.Password = PasswordGenerator.GenerateRandomPassword();
                userRegister.RePassword = userRegister.Password;
            }

            if (userRegister.Password != userRegister.RePassword)
            {
                message = new ResponseMessageModel{
                    Code = MessageCode.DATA_VALIDATE_ERROR,
                    ErrorMessage = "Mật khẩu không khớp nhau"
                };
                return message;
            }
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = userRegister.UserName,
                PersonalId = userRegister.PersonalId,
                PasswordChanged = false
            };

            IdentityResult result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                var userInfo = new UserRegistedModel
                {
                    UserName = user.UserName,
                    Id = user.Id,
                    Password = userRegister.Password
                };
                message = new ResponseMessageModel{
                    Code = MessageCode.SUCCESS,
                    Data = userInfo
                };
                return message;
            }
            //errors occur
            message = new ResponseMessageModel{
                Code = MessageCode.SQL_ACTION_ERROR,
                ErrorMessage = result.Errors.ToString()
            };
            return message;
        }

        [Route("{userId}")]
        [HttpDelete]
        [Authorize(Roles = "manager")]
        public async Task<object> RemoveUser(string userId)
        {
            ResponseMessageModel message;
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    message = new ResponseMessageModel{
                        Code = MessageCode.SUCCESS
                    };
                    return message;
                }
                else
                {
                    string errors = "";
                    foreach(var e in result.Errors){
                        errors += e.Description + "\r\n";
                    }
                    message = new ResponseMessageModel{
                        Code = MessageCode.SQL_ACTION_ERROR,
                        ErrorMessage = errors
                    };
                    return message;
                }
            }
            message = new ResponseMessageModel{
                Code = MessageCode.OBJECT_NOT_FOUND
            };
            return message;
        }

        [Route("{userId}/reset")]
        [HttpPut]
        [Authorize(Roles="manager")]
        public async Task<object> ResetPassword(Guid userId){
            ResponseMessageModel message;
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if(user == null){
                message = ResponseMessageModel.CreateResponse(
                    MessageCode.OBJECT_NOT_FOUND,
                    "Không tìm thấy user");
                return message;
            }

            //generate new password
            string newPassword = PasswordGenerator.GenerateRandomPassword();
            //generate token and reset password
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user,token,newPassword);
            if(!result.Succeeded){
                message = ResponseMessageModel.CreateResponse(
                    MessageCode.SQL_ACTION_ERROR,
                    "Không thể khởi tạo password mới");
                return message;
            }
            //reset success
            var userRegistedModel = new UserRegistedModel{
                UserName = user.UserName,
                Id = user.Id,
                Password = newPassword
            };
            message = ResponseMessageModel.CreateResponse(userRegistedModel);
            return message;
        }

#if DEBUG
        [Route("generate")]
        [HttpPost]
        public async Task<object> GenerateManagerAccount()
        {
            var dbContext = (DbContext)HttpContext.RequestServices.GetService(typeof(ApplicationDbContext));
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    //create role
                    var role = new ApplicationRole
                    {
                        Name = "manager",
                        Id = Guid.Parse("9c7401dd-f626-43cd-ae1b-5615df9fadcb")
                    };

                    var result = await _roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                        throw new ApplicationException("CREATE_ROLE_FAILD");

                    //create administrator account
                    var user = new ApplicationUser
                    {
                        Id = Guid.Parse("9c547812-b4dc-4efd-8d17-a12b639adaa5"),
                        UserName = "dinhhongphi",
                        PasswordChanged = true,
                        PersonalId = Guid.Parse("1a29080a-2644-4e60-8995-3b3865d73b27")
                    };
                    result = await _userManager.CreateAsync(user, "Dinhhongphi@017");

                    if (!result.Succeeded)
                        throw new ApplicationException("CREATE_ACCOUNT_FAILD");

                    //assign role to account
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                    if (!result.Succeeded)
                        throw new ApplicationException("ASSIGN_ROLE_FAILD");
                    transaction.Commit();
                    return NoContent();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ApplicationException(ex.Message);
                }
            }
        }

#endif

        private async Task<object> GenerateJwtToken(string username, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            //Add roles to token
            var roles = await _userManager.GetRolesAsync(user);
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //Add full name to token
            var personal = _personalService.Find(user.PersonalId);
            claims.Add(new Claim(ClaimTypes.Name,personal.FullName));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}