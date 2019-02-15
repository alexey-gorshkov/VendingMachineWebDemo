using System;
using System.ComponentModel.DataAnnotations;
using VendingMachine.Core.Models;
using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Entities
{
    public class PurseCoin : BaseEntity<Guid>
    {
        [Required]
        public Guid PurseId { get; set; }

        public TypeCoin TypeCoin { get; set; }

        public int Count { get; set; }
    }
}
