using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VendingMachine.DAL.Entities;
using VendingMachine.WebAPI.Models;

namespace VendingMachine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(IConfiguration configuration,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET api/values
        [HttpPost, Route("login")]
        public async Task<TokenResult> LoginAsync([FromBody]LoginModel model)
        {
            //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            var user = await _userManager.FindByEmailAsync(model.UserName);
            if (user == null)
            {
                return new TokenResult("Invalid client request");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                var token = GenerateJwtToken();
                return new TokenResult("", token, true);
            }
            if (result.IsNotAllowed)
            {
                return new TokenResult("IsNotAllowed");
            }
            if (result.IsLockedOut)
            {
                return new TokenResult("YourAccountLocked");
            }

            return new TokenResult("IncorrectLoginData");
        }


        public string GenerateJwtToken()
        {
            // get options
            var jwtAppSettingOptions = _configuration.GetSection("JwtIssuerOptions");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAppSettingOptions["JwtKey"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(jwtAppSettingOptions["JwtExpireDays"]));

            var tokeOptions = new JwtSecurityToken(
                issuer: jwtAppSettingOptions["JwtIssuer"],
                audience: jwtAppSettingOptions["JwtIssuer"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}