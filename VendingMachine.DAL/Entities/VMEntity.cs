using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VendingMachine.DAL.Entities
{
    public class VMEntity : BaseEntity<Guid>
    {
        [Required]
        public Guid UserAdminId { get; set; }
        public User UserAdmin { get; set; }

        public string Name { get; set; }

        public virtual ICollection<VMCreator> Creators { get; set; }
    }
}
