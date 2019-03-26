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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly UserManager<User> _userManager;

        public PaymentController(IPaymentService paymentService,
            UserManager<User> userManager)
        {
            _paymentService = paymentService;
            _userManager = userManager;
        }

        // PUT: api/Payment/GetDepositCustomer
        [HttpPut, Route("GetDepositCustomer")]
        public async Task<ActionResult<UserDeposit>> GetDepositCustomerAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                BadRequest();
            }

            try
            {
                await _paymentService.GetDepositCustomerAsync(Guid.Parse(userId));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Payment/AddAmountDeposit
        [HttpPost, Route("AddAmountDeposit")]
        public async Task<ActionResult> AddAmountDeposit(CoinDTO coin)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                BadRequest();
            }

            try
            {
                await _paymentService.AddAmountDepositAsync(coin, Guid.Parse(userId));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Payment/BuyProduct
        [HttpPost, Route("BuyProduct")]
        public async Task<ActionResult<ProductDTO>> BuyProduct(CreatorProductDTO creatorProductDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                BadRequest("Invalid user");
            }

            try
            {
                return await _paymentService.BuyProduct(Guid.Parse(userId), creatorProductDTO.TypeProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
