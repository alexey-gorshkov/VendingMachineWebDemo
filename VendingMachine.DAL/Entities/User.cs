using Microsoft.AspNetCore.Identity;
using System;

namespace VendingMachine.DAL.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser<Guid>
    {
        public Purse Purse { get; set; }
    }
}
