using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myApplication.Dal.Models;
using myApplication.Dal.ViewModels;
using myApplication.api.Helper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace myApplication.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        
        private readonly UserManager<applicationUser> _userManager;
        private readonly SignInManager<applicationUser> _signInManager;
        private readonly applicationSettings _applicationSettings;

        public AccountController(UserManager<applicationUser> userManager, SignInManager<applicationUser> signInManager, IOptions<applicationSettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationSettings = options.Value;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<Object> PostApplicationUser(registerViewModel model)
        {
            var applicationUser = new applicationUser()
            {
                UserName = model.userName,
                Email = model.email,
                age = model.age
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.password);
                await _userManager.AddToRoleAsync(applicationUser, "customer");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(loginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.userName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();

                List<Claim> claims = new List<Claim>
                 {
                        new Claim("UserID",user.Id.ToString()),

                 };

                foreach (var item in roles)
                {
                    claims.Add(new Claim(_options.ClaimsIdentity.RoleClaimType, item));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        claims
                        ),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettings.secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }



    }
}
