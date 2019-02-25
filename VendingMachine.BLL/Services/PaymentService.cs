using System;
using System.Threading.Tasks;
using VendingMachine.BLL.DTO;
using VendingMachine.BLL.Interfaces;
using VendingMachine.Core.Models;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPurseRepository _purseRepository;
        private readonly IUserDepositRepository _userDepositRepository;
        private readonly ICustomerProductRepository _customerProductRepository;
        private readonly IVendingMachineService _vendingMachineService;

        public PaymentService(IPurseRepository purseRepository,
            IUserDepositRepository userDepositRepository,
            ICustomerProductRepository customerProductRepository,
            IVendingMachineService vendingMachineService)
        {
            _purseRepository = purseRepository;
            _userDepositRepository = userDepositRepository;
            _customerProductRepository = customerProductRepository;
            _vendingMachineService = vendingMachineService;
        }

        // покупатель внес монету в монетоприемник (deposit)
        public async Task AddAmountDepositAsync(CoinDTO coin, Guid userId)
        {
            if (coin == null)
            {
                throw new NullReferenceException("Coin is null");
            }

            // увеличиваем сумму депозита юзера
            await _userDepositRepository.AddAmountDepositAsync((int)coin.TypeCoin, userId);

            // забираем у юзеру монетку
            await _purseRepository.RemoveCoinAsync(userId, coin.TypeCoin);

            // отдаем монетку VM
            await _vendingMachineService.AddCoinAsync(coin.TypeCoin);
        }

        public async Task<ProductDTO> BuyProduct(Guid userId, TypeProduct typeProduct)
        {
            var creatorProduct = await _vendingMachineService.GetInfoProductAsync(typeProduct);
            var depositCustomer = await _userDepositRepository.GetAmountDepositAsync(userId);

            if (creatorProduct.Product.Price > depositCustomer)
            {
                throw new ApplicationException("The amount of the deposit is less than the value of the goods");
            }

            // уменьшаем депозит покупателя на сумму товара
            await _userDepositRepository.RetrieveDepositAsync(userId, creatorProduct.Product.Price);
            // выдать товар из машины
            var product = await _vendingMachineService.CreateProductAsync(typeProduct);

            var customerProduct = new DAL.Entities.CustomerProduct
            {
                CustomerId = userId,
                Name = product.Name,
                Price = product.Price
            };

            // сохранить в историю юзера выданный товар
            await _customerProductRepository.Create(customerProduct);

            return product;
        }

        // вернуть сдачу покупателю
        public async Task GetDepositCustomerAsync(Guid userId)
        {
            // находим сумму депозита
            var amoutDeposit = await _userDepositRepository.GetAmountDepositAsync(userId);

            // списываем депозит юзера
            await _userDepositRepository.RetrieveDepositAsync(userId);

            // забираем монетки у VM
            var coins = await _vendingMachineService.RetrieveCoinsAsync(amoutDeposit);

            // отдаем монетку Customer
            await _purseRepository.AddCoinsAsync(userId, coins);
        }
    }
}
