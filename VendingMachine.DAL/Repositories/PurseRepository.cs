using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Purse> GetPurseAndCoinsAsync(Guid customerId)
        {
            return await GetAll().Include(x=>x.PurseCoins)
                .SingleOrDefaultAsync(x => x.ApplicationUserId == customerId);
        }
    }
}
