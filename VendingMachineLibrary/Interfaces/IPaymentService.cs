using System;
using System.Threading.Tasks;
using VendingMachine.BLL.DTO;

namespace VendingMachine.BLL.Interfaces
{
    public interface IPaymentService
    {
        Task AddAmountDepositAsync(CoinDTO coin, Guid userId);
        Task GetDepositCustomerAsync(Guid userId);
    }
}
