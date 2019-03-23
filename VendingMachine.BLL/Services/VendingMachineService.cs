using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.BLL.DTO;
using VendingMachine.BLL.Factories.Creators;
using VendingMachine.BLL.Factories.Products;
using VendingMachine.BLL.Interfaces;
using VendingMachine.Core.Models;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.BLL.Services
{
    public class VendingMachineService : IVendingMachineService
    {
        const string VMUSERNAME = "vending-machine@vmachine.com";

        private readonly IPurseRepository _purseRepository;
        private readonly IUserDepositRepository _userDepositRepository;
        private readonly IVMCreatorRepository _vMCreatorRepository;
        private readonly ICustomerProductRepository _customerProductRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public VendingMachineService(IPurseRepository purseRepository,
            IUserDepositRepository userDepositRepository,
            IVMCreatorRepository vMCreatorRepository,
            ICustomerProductRepository customerProductRepository,
            IMapper mapper,
            UserManager<User> userManager)
        {
            _purseRepository = purseRepository;
            _userDepositRepository = userDepositRepository;
            _vMCreatorRepository = vMCreatorRepository;
            _customerProductRepository = customerProductRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<VendingMachineStateDTO> GetUserStateAsync(User customer)
        {
            var deposit = await _userDepositRepository.GetAll()
                .Where(x => x.UserId == customer.Id)
                .SingleOrDefaultAsync();

            var creators = await _vMCreatorRepository.GetAll().ToListAsync();
            var creatorsDTO = _mapper.Map<List<VMCreator>, List<CreatorProductDTO>>(creators);
            var purseCustomer = await _purseRepository.GetPurseAndCoinsAsync(customer.Id);
            var customerProducts = await _customerProductRepository.GetAll()
                .Where(x => x.CustomerId == customer.Id)
                .ToListAsync();
            var vmUser = await _userManager.FindByNameAsync(VMUSERNAME);
            var purseVM = await _purseRepository.GetPurseAndCoinsAsync(vmUser.Id);

            return new VendingMachineStateDTO
            {
                Customer = new CustomerDTO
                {
                    AmountDeposited = deposit?.AmountOfDeposit ?? 0,
                    Purse = _mapper.Map<Purse, PurseDTO>(purseCustomer),
                    Products = _mapper.Map<List<CustomerProduct>, List<CustomerProductDTO>>(customerProducts)
                },
                VendingMachine = new VendingMachineDTO
                {
                    CreatorProducts = creatorsDTO,
                    Purse = _mapper.Map<Purse, PurseDTO>(purseVM)
                }
            };
        }

        public async Task AddCoinAsync(TypeCoin typeCoin)
        {
            var vmUser = await _userManager.FindByNameAsync(VMUSERNAME);
            await _purseRepository.AddCoinAsync(vmUser.Id, typeCoin);
        }

        public async Task<IEnumerable<Coin>> RetrieveCoinsAsync(int sum)
        {
            var vmUser = await _userManager.FindByNameAsync(VMUSERNAME);
            return await _purseRepository.RemoveCoinsAsync(vmUser.Id, sum);
        }

        // выдача товара из машины
        public async Task<ProductDTO> CreateProductAsync(TypeProduct typeProduct)
        {
            var creatorEntity = await _vMCreatorRepository.FirstOrDefaultAsync(x => x.TypeProduct == typeProduct);
            if (creatorEntity == null)
            {
                throw new ApplicationException("This product cannot be created");
            }

            CreatorBase creatorFactory = (CreatorBase)Activator.CreateInstance(Type.GetType(creatorEntity.CreatorClassName), creatorEntity.Availability);
            // готовим продукт использую фабричный метод
            creatorFactory.Create();

            return new ProductDTO { Name = creatorEntity.Name, Price = creatorEntity.Price };
        }

        public async Task<CreatorProductDTO> GetInfoProductAsync(TypeProduct typeProduct)
        {
            VMCreator creatorEntity = await _vMCreatorRepository.FirstOrDefaultAsync(x => x.TypeProduct == typeProduct);
            if (creatorEntity == null)
            {
                throw new ApplicationException("This product cannot be created");
            }
            return _mapper.Map<VMCreator, CreatorProductDTO>(creatorEntity);
        }
    }
}
