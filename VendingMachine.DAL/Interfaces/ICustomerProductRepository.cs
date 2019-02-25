using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.Core.Models;
using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Interfaces
{
    public interface ICustomerProductRepository : IRepository<CustomerProduct, Guid>
    {
    }
}
