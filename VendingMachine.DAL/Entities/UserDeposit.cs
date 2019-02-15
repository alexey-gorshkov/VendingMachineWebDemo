using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.DAL.Entities
{
    public class UserDeposit : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public int AmountOfDeposit { get; set; }
    }
}
