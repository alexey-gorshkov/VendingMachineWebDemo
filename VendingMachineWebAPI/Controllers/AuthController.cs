using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
                return GenerateJwtToken(user.Email, user.Id);
            }
            if (result.IsNotAllowed)
            {
                return new TokenResult("IsNotAllowed");
            }
            if (result.IsLockedOut)
            {
                return new TokenResult("Your Account Locked");
            }

            return new TokenResult("Incorrect Login Data");
        }


        public TokenResult GenerateJwtToken(string email, Guid userId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            // get options
            var jwtAppSettingOptions = _configuration.GetSection("JwtIssuerOptions");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAppSettingOptions["JwtKey"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddSeconds(Convert.ToDouble(jwtAppSettingOptions["JwtExpireSeconds"]));

            var tokeOptions = new JwtSecurityToken(
                issuer: jwtAppSettingOptions["JwtIssuer"],
                audience: jwtAppSettingOptions["JwtIssuer"],
                claims: claims,
                expires: expires,
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return new TokenResult(tokenString, Convert.ToInt32(jwtAppSettingOptions["JwtExpireSeconds"]));
        }
    }
}