using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VendingMachine.DAL.Entities
{
    public class Purse : BaseEntity<Guid>
    {
        [Required]
        public Guid ApplicationUserId { get; set; }
        public User ApplicationUser { get; set; }

        // my coins
        public virtual ICollection<PurseCoin> PurseCoins { get; set; }
    }
}
