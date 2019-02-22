using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Core.Models;
using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Interfaces
{
    public interface IPurseRepository : IRepository<Purse, Guid>
    {
        /// <summary>
        /// Получаем кошелек и монеты в нем
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Purse> GetPurseAndCoinsAsync(Guid userId);

        /// <summary>
        /// Добавляем монетку в кошелек
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="typeCoin"></param>
        /// <returns></returns>
        Task AddCoinAsync(Guid userId, TypeCoin typeCoin);

        /// <summary>
        /// Добавляем монеты
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="coins"></param>
        /// <returns></returns>
        Task AddCoinsAsync(Guid userId, IEnumerable<Coin> coins);

        /// <summary>
        /// Удаляем монету по типу
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="coin"></param>
        /// <returns></returns>
        Task RemoveCoinAsync(Guid userId, TypeCoin typeCoin);

        /// <summary>
        /// Удаляем монеты по сумме
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        Task<IEnumerable<Coin>> RemoveCoinsAsync(Guid userId, int sum);
    }
}
