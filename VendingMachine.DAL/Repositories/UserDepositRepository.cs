using System;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.DAL.Repositories
{
    public class UserDepositRepository : BaseRepository<UserDeposit, Guid>, IUserDepositRepository
    {
        public UserDepositRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
