using System;
using System.ComponentModel.DataAnnotations;

namespace VendingMachine.DAL.Entities
{
    public class UserDeposit : BaseEntity<Guid>
    {
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public int AmountOfDeposit { get; set; }
    }
}
