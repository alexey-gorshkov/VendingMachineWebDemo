using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.BLL.DTO;
using VendingMachine.BLL.Interfaces;
using VendingMachine.Core.Models;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.Tests.VM.BLL.Services.Fake
{
    public class VendingMachineServiceFake : IVendingMachineService
    {
        const string VMUSERNAME = "vending-machine@vmachine.com";

        private readonly IPurseRepository _purseRepository;
        private readonly ApplicationDbContext _context;

        public VendingMachineServiceFake(IPurseRepository purseRepository,
            ApplicationDbContext context)
        {
            _purseRepository = purseRepository;
            _context = context;
        }

        public async Task AddCoinAsync(TypeCoin typeCoin)
        {
            var vmUser = await _context.Users.FirstOrDefaultAsync(x=> x.UserName == VMUSERNAME);
            await _purseRepository.AddCoinAsync(vmUser.Id, typeCoin);
        }

        public Task<ProductDTO> CreateProductAsync(TypeProduct typeProduct)
        {
            throw new NotImplementedException();
        }

        public Task<CreatorProductDTO> GetInfoProductAsync(TypeProduct typeProduct)
        {
            throw new NotImplementedException();
        }

        public Task<VendingMachineStateDTO> GetUserStateAsync(User customer)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Coin>> RetrieveCoinsAsync(int sum)
        {
            throw new NotImplementedException();
        }
    }
}
