using System;
using System.Threading.Tasks;
using VendingMachine.BLL.DTO;
using VendingMachine.BLL.Interfaces;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPurseRepository _purseRepository;
        private readonly IUserDepositRepository _userDepositRepository;
        private readonly IVendingMachineService _vendingMachineService;

        public PaymentService(IPurseRepository purseRepository,
            IUserDepositRepository userDepositRepository,
            IVendingMachineService vendingMachineService)
        {
            _purseRepository = purseRepository;
            _userDepositRepository = userDepositRepository;
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

        // вернуть сдачу покупателю
        public async Task GetDepositCustomerAsync(Guid userId)
        {
            // находим сумму депозита
            var amoutDeposit = await _userDepositRepository.GetAmountDepositAsync(userId);

            // списываем депозит юзера
            await _userDepositRepository.RetrieveDepositAsync(userId);

            // забираем у монетки у VM
            var coins = await _vendingMachineService.RetrieveCoinsAsync(amoutDeposit);

            // отдаем монетку Customer
            await _purseRepository.AddCoinsAsync(userId, coins);
        }


    }
}
