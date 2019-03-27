using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VendingMachine.BLL.Interfaces;
using VendingMachine.Core.Models;
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
        private readonly IEmailService _emailService;

        public AuthController(IConfiguration configuration,
            UserManager<User> userManager, SignInManager<User> signInManager,
            IEmailService emailService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        // GET api/auth
        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<TokenResultModel> LoginAsync([FromBody]LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMsgModelArray = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return new TokenResultModel(errorMsgModelArray);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new TokenResultModel("Invalid client request");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                return GenerateJwtToken(user.Email, user.Id);
            }
            if (result.IsNotAllowed)
            {
                return new TokenResultModel("IsNotAllowed");
            }
            if (result.IsLockedOut)
            {
                return new TokenResultModel("Your Account Locked");
            }

            return new TokenResultModel("Incorrect Login Data");
        }

        [AllowAnonymous]
        [HttpPost, Route("register")]
        public async Task<RegisterResultModel> Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMsgModelArray = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return new RegisterResultModel(errorMsgModelArray);
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // новый пользователь и даем ему кошелек
                    var user = new User {
                        UserName = model.Email,
                        Email = model.Email,
                        Purse = new Purse
                        {
                            PurseCoins = new List<PurseCoin>
                            {
                                new PurseCoin { TypeCoin = TypeCoin.Price1Rub, Count = 100 },
                                new PurseCoin { TypeCoin = TypeCoin.Price2Rub, Count = 100 },
                                new PurseCoin { TypeCoin = TypeCoin.Price5Rub, Count = 100 },
                                new PurseCoin { TypeCoin = TypeCoin.Price10Rub, Count = 100 }
                            }
                        }
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        await _emailService.SendConfirmEmailLinkAsync(user.Id, model.Email, code).ConfigureAwait(false);

                        // Commit transaction if all commands succeed, transaction will auto-rollback
                        // when disposed if either commands fails
                        scope.Complete();

                        return new RegisterResultModel("A message with a token sent to your email", true);
                    }
                    var errorMsgArray = result.Errors.Select(x => x.Description).ToArray();
                    return new RegisterResultModel(string.Join(",", errorMsgArray));
                }
                catch (Exception ex)
                {
                    return new RegisterResultModel(ex.Message);
                }
            }
        }

        [AllowAnonymous]
        [HttpGet, Route("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest("Token invalid");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                // redirect to frontend
                return RedirectPermanent("http://localhost:4200/");
            }

            var errorMsgArray = result.Errors.Select(x => x.Description).ToArray();
            return BadRequest(string.Join(",", errorMsgArray));
        }

        private TokenResultModel GenerateJwtToken(string email, Guid userId)
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
            return new TokenResultModel(tokenString, Convert.ToInt32(jwtAppSettingOptions["JwtExpireSeconds"]));
        }
    }
}