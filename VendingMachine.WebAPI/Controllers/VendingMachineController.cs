﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly UserManager<User> _userManager;
        private readonly IVendingMachineService _vendingMachineService;

        public VendingMachineController(UserManager<User> userManager,
            IVendingMachineService vendingMachineService)
        {
            _userManager = userManager;
            _vendingMachineService = vendingMachineService;
        }

        // GET: api/VendingMachine
        [HttpGet]
        public async Task<VendingMachineStateDTO> GetAsync()
        {
            var customer = await _userManager.GetUserAsync(User);
            return await _vendingMachineService.GetUserStateAsync(customer);
        }
    }
}
