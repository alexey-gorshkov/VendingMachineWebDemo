using System;
using System.Threading.Tasks;
using System.Transactions;
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

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // увеличиваем сумму депозита юзера
                    await _userDepositRepository.AddAmountDepositAsync((int)coin.TypeCoin, userId);

                    // забираем у юзеру монетку
                    await _purseRepository.RemoveCoinAsync(userId, coin.TypeCoin);

                    // TODO: To verify a transaction, you need to uncomment this line ->
                    // throw new Exception("To verify a transaction, you need to uncomment this line.");

                    // отдаем монетку VM
                    await _vendingMachineService.AddCoinAsync(coin.TypeCoin);

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    scope.Complete();
                }
                catch (System.Exception)
                {
                    // TODO: Handle failure
                    throw new ApplicationException("The problem of adding a user deposit amount!");
                }
            };
        }

        public async Task<ProductDTO> BuyProduct(Guid userId, TypeProduct typeProduct)
        {
            ProductDTO product = null;
            var creatorProduct = await _vendingMachineService.GetInfoProductAsync(typeProduct);
            var depositCustomer = await _userDepositRepository.GetAmountDepositAsync(userId);

            if (creatorProduct.Product.Price > depositCustomer)
            {
                throw new ApplicationException("The amount of the deposit is less than the value of the goods");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // уменьшаем депозит покупателя на сумму товара
                    await _userDepositRepository.RetrieveDepositAsync(userId, creatorProduct.Product.Price);
                    // выдать товар из машины
                    product = await _vendingMachineService.CreateProductAsync(typeProduct);

                    var customerProduct = new DAL.Entities.CustomerProduct
                    {
                        CustomerId = userId,
                        Name = product.Name,
                        Price = product.Price
                    };

                    // сохранить в историю юзера выданный товар
                    await _customerProductRepository.Create(customerProduct);

                    scope.Complete();
                }
                catch (System.Exception)
                {
                    // TODO: Handle failure
                    throw new ApplicationException("The problem of buying product!");
                }
            }

            return product;
        }

        // вернуть сдачу покупателю
        public async Task GetDepositCustomerAsync(Guid userId)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // находим сумму депозита
                    var amoutDeposit = await _userDepositRepository.GetAmountDepositAsync(userId);

                    // списываем депозит юзера
                    await _userDepositRepository.RetrieveDepositAsync(userId);

                    // забираем монетки у VM
                    var coins = await _vendingMachineService.RetrieveCoinsAsync(amoutDeposit);

                    // отдаем монетку Customer
                    await _purseRepository.AddCoinsAsync(userId, coins);

                    scope.Complete();
                }
                catch (System.Exception)
                {
                    // TODO: Handle failure
                    throw new ApplicationException("The problem of receiving a deposit by the user!");
                }
            }
        }
    }
}
