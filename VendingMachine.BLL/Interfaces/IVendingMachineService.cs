using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.BLL.DTO;
using VendingMachine.Core.Models;
using VendingMachine.DAL.Entities;

namespace VendingMachine.BLL.Interfaces
{
    public interface IVendingMachineService
    {
        Task<VendingMachineStateDTO> GetUserStateAsync(User customer);
        /// <summary>
        /// Добавим монетки
        /// </summary>
        /// <param name="typeCoin"></param>
        /// <returns></returns>
        Task AddCoinAsync(TypeCoin typeCoin);
        /// <summary>
        /// Спишем монетки
        /// </summary>
        /// <param name="sum"></param>
        /// <returns></returns>
        Task<IEnumerable<Coin>> RetrieveCoinsAsync(int sum);
        Task<CreatorProductDTO> GetInfoProductAsync(TypeProduct typeProduct);
        Task<ProductDTO> CreateProductAsync(TypeProduct typeProduct);
    }
}
