using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace VendingMachine.DAL.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser<Guid>
    {
        public Purse Purse { get; set; }
        public UserDeposit UserDeposit { get; set; }

        public virtual ICollection<CustomerProduct> CustomerProducts { get; set; }
    }
}
