using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VendingMachine.BLL.DTO;
using VendingMachine.BLL.Interfaces;
using VendingMachine.DAL.Entities;

namespace VendingMachine.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDepositController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly UserManager<User> _userManager;

        public CustomerDepositController(IPaymentService paymentService,
            UserManager<User> userManager)
        {
            _paymentService = paymentService;
            _userManager = userManager;
        }

        // GET: api/CustomerDeposit
        [HttpGet]
        public async Task<ActionResult<int>> GetDeposits()
        {
            var customer = await _userManager.GetUserAsync(User);
            return customer.UserDeposit.AmountOfDeposit;
        }

        // POST: api/CustomerDeposit
        [HttpPost]
        public async Task<ActionResult> PostCustomerDeposit(CoinDTO coin)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                BadRequest();
            }

            await _paymentService.AddAmountDepositAsync(coin, Guid.Parse(userId));
            return Ok();
        }

        // DELETE: api/CustomerDeposit
        [HttpDelete]
        public async Task<ActionResult<UserDeposit>> DeleteCustomerDeposit()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                BadRequest();
            }

            await _paymentService.GetDepositCustomerAsync(Guid.Parse(userId));
            return Ok();
        }

        //private bool UserDepositExists(Guid id)
        //{
        //    return _context.UserDeposits.Any(e => e.Id == id);
        //}
    }
}
