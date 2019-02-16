using System;
using System.Threading.Tasks;
using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Interfaces
{
    public interface IPurseRepository : IRepository<Purse, Guid>
    {
        Task<Purse> GetPurseAndCoinsAsync(Guid customerId);
    }
}
