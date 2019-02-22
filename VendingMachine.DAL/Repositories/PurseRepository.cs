using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Core.Models;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.DAL.Repositories
{
    public class PurseRepository : BaseRepository<Purse, Guid>, IPurseRepository
    {
        public PurseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Purse> GetPurseAndCoinsAsync(Guid userId)
        {
            return await GetAll()
                .Include(x => x.PurseCoins)
                .SingleOrDefaultAsync(x => x.ApplicationUserId == userId);
        }

        public async Task AddCoinAsync(Guid userId, TypeCoin typeCoin)
        {
            await AddCoinsAsync(userId, new List<Coin> { new Coin(typeCoin) });
        }

        public async Task AddCoinsAsync(Guid userId, IEnumerable<Coin> coins)
        {
            var purse = await GetAll()
                .Include(x => x.PurseCoins)
                .SingleOrDefaultAsync(x => x.ApplicationUserId == userId);

            foreach (var coinsGroup in coins.GroupBy(x => x.TypeCoin))
            {
                var findCoin = purse.PurseCoins.FirstOrDefault(x => x.TypeCoin == coinsGroup.Key);
                if (findCoin == null)
                {
                    findCoin = new PurseCoin
                    {
                        TypeCoin = coinsGroup.Key
                    };
                    purse.PurseCoins.Add(findCoin);
                }
                findCoin.Count += coinsGroup.Count();
            }
            await Update(purse);
        }

        public async Task RemoveCoinAsync(Guid userId, TypeCoin typeCoin)
        {
            var purse = await GetAll()
                .Include(x => x.PurseCoins)
                .SingleOrDefaultAsync(x => x.ApplicationUserId == userId);

            PurseCoin findCoins = purse.PurseCoins.FirstOrDefault(x => x.TypeCoin == typeCoin);

            if (findCoins == null || findCoins.Count == 0)
            {
                throw new ApplicationException("This coin is not found");
            }

            findCoins.Count--;
            await Update(purse);
        }

        public async Task<IEnumerable<Coin>> RemoveCoinsAsync(Guid userId, int sum)
        {
            Purse purse = await GetAll()
                .Include(x => x.PurseCoins)
                .SingleOrDefaultAsync(x => x.ApplicationUserId == userId);

            var prepareCoins = GetCoinsBySum(purse, sum);
            if (prepareCoins.Sum(x=>x.Price) != sum)
            {
                throw new ApplicationException("The amount can not be refunded. The wallet does not have a lot of coins.");
            }

            foreach (var coinsGroup in prepareCoins.GroupBy(x=>x.TypeCoin))
            {
                var findCoin = purse.PurseCoins.FirstOrDefault(x => x.TypeCoin == coinsGroup.Key);
                findCoin.Count -= coinsGroup.Count();
            }

            await Update(purse);
            return prepareCoins;
        }

        private IEnumerable<Coin> GetCoinsBySum(Purse purse, int sum)
        {
            List<Coin> resultList = new List<Coin>();

            // сортируем монеты по убыванию
            foreach (var purseCoin in purse.PurseCoins.OrderByDescending(x => x.TypeCoin))
            {
                while (sum > 0 && (sum - purseCoin.Price) >= 0)
                {
                    sum -= purseCoin.Price;
                    resultList.Add(new Coin(purseCoin.TypeCoin));
                }

                if (sum == 0)
                {
                    break;
                }
            }

            return resultList;
        }
    }
}
