using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VendingMachine.DAL.Data;
using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Entities
{
    public class VendingMachineState : BaseEntity<Guid>
    {
        [Required]
        public Guid ApplicationUserId { get; set; }
        public User ApplicationUser { get; set; }



    }
}
