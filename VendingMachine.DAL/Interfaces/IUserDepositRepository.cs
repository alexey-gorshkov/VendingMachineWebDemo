using System;
using System.Threading.Tasks;
using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Interfaces
{
    public interface IUserDepositRepository : IRepository<UserDeposit, Guid>
    {
        Task<int> GetAmountDepositAsync(Guid userId);
        Task AddAmountDepositAsync(int sum, Guid userId);
        Task RetrieveDepositAsync(Guid userId);
        Task RetrieveDepositAsync(Guid userId, int sum);
    }
}
