using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using QuanLyNongTrai.Model;
using QuanLyNongTrai.Model.Entity;
using QuanLyNongTrai.UI.Entity;

namespace QuanLyNongTrai
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [Route("token")]
        [HttpPost]
        public async Task<object> Login([FromBody]UserLoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.UserName);
                var token = new TokenModel
                {
                    Token = (string)(await GenerateJwtToken(model.UserName, appUser))
                };
                return Json(token);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<object> Register([FromBody]UserRegisterModel userRegister)
        {
            if (userRegister.Password != userRegister.RePassword)
            {
                throw new ValidationException("Mật khẩu không khớp");
            }
            var user = new ApplicationUser
            {
                UserName = userRegister.UserName,
                PersonalId = userRegister.PersonalId
            };

            IdentityResult result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                return NoContent();
            }
            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [Route("{userId}")]
        [HttpDelete]
        [Authorize(Roles = "manager")]
        public async Task<object> RemoveUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return NoContent();
                }
                else
                {
                    throw new ApplicationException();
                }
            }
            throw new KeyNotFoundException("Không tìm thấy user với Id được cung cấp");
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

            var roles = await _userManager.GetRolesAsync(user);
            foreach(string role in roles){
                claims.Add(new Claim(ClaimTypes.Role,role));
            }
            
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