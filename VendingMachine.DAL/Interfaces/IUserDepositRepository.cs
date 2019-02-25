using System;
using System.Threading.Tasks;
using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Interfaces
{
    public interface IUserDepositRepository : IRepository<UserDeposit, Guid>
    {
        /// <summary>
        /// Сколько у юзера на депозите
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> GetAmountDepositAsync(Guid userId);
        Task AddAmountDepositAsync(int sum, Guid userId);
        /// <summary>
        /// Спишем весь депозит (сдача)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task RetrieveDepositAsync(Guid userId);
        /// <summary>
        /// Спишем депозит на сумму
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        Task RetrieveDepositAsync(Guid userId, int sum);
    }
}
