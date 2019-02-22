using System;
using System.Threading.Tasks;
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

        public async Task AddAmountDepositAsync(int sum, Guid userId)
        {
            var deposit = await FirstOrDefaultAsync(x => x.UserId == userId);
            if (deposit == null)
            {
                deposit = new UserDeposit
                {
                    UserId = userId
                };
            }

            deposit.AmountOfDeposit += sum;
            await Update(deposit);
        }

        public async Task<int> GetAmountDepositAsync(Guid userId)
        {
            var deposit = await FirstOrDefaultAsync(x => x.UserId == userId);
            return deposit?.AmountOfDeposit ?? 0;
        }

        // спишет весь депозит
        public async Task RetrieveDepositAsync(Guid userId)
        {
            var deposit = await FirstOrDefaultAsync(x => x.UserId == userId);
            if (deposit == null)
            {
                deposit = new UserDeposit
                {
                    UserId = userId
                };
            }

            deposit.AmountOfDeposit = 0;
            await Update(deposit);
        }

        public async Task RetrieveDepositAsync(Guid userId, int sum)
        {
            var deposit = await FirstOrDefaultAsync(x => x.UserId == userId);
            if (deposit == null)
            {
                deposit = new UserDeposit
                {
                    UserId = userId
                };
            }

            if (sum > deposit.AmountOfDeposit)
            {
                throw new ApplicationException("The amount to issue more than the amount of the user's deposit");
            }

            deposit.AmountOfDeposit -= sum;
            await Update(deposit);
        }
    }
}
