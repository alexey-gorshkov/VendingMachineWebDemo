using System;
using System.ComponentModel.DataAnnotations;

namespace VendingMachine.DAL.Entities
{
    public class CustomerProduct : BaseEntity<Guid>
    {
        [Required]
        public Guid CustomerId { get; set; }
        public User Customer { get; set; }

        [Required]
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
