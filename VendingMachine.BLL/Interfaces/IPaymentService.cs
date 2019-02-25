using System;
using System.Threading.Tasks;
using VendingMachine.BLL.DTO;
using VendingMachine.Core.Models;

namespace VendingMachine.BLL.Interfaces
{
    public interface IPaymentService
    {
        Task AddAmountDepositAsync(CoinDTO coin, Guid userId);
        Task GetDepositCustomerAsync(Guid userId);
        Task<ProductDTO> BuyProduct(Guid userId, TypeProduct typeProduct);
    }
}
