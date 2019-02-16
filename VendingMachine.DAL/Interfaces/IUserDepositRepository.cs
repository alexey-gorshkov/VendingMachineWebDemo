using System;
using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Interfaces
{
    public interface IUserDepositRepository : IRepository<UserDeposit, Guid>
    {
    }
}
