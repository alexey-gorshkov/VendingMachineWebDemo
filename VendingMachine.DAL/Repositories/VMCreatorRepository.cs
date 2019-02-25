using System;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.DAL.Repositories
{
    public class VMCreatorRepository : BaseRepository<VMCreator, Guid>, IVMCreatorRepository
    {
        public VMCreatorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
