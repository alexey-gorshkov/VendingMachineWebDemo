using System;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.DAL.Repositories
{
    public class CustomerProductRepository : BaseRepository<CustomerProduct, Guid>, ICustomerProductRepository
    {
        public CustomerProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
