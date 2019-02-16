using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VendingMachine.BLL.DTO;
using VendingMachine.BLL.Interfaces;
using VendingMachine.DAL.Entities;

namespace VendingMachine.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VendingMachineController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IVendingMachineService _vendingMachineService;

        public VendingMachineController(IMapper mapper,
            UserManager<User> userManager,
            IVendingMachineService vendingMachineService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _vendingMachineService = vendingMachineService;
        }

        // GET: api/VendingMachine
        [HttpGet]
        public async Task<VendingMachineStateDTO> GetASync()
        {
            // user ----------
            // свой кошелек
            // депозит
            // купленные товары

            // vm ------------
            // доступные товары

            var customer = await _userManager.GetUserAsync(User);
            return await _vendingMachineService.GetUserStateAsync(customer);
        }

        // GET: api/VendingMachine/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/VendingMachine
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/VendingMachine/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
