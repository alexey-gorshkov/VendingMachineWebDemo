using System;
using System.ComponentModel.DataAnnotations;
using VendingMachine.Core.Models;

namespace VendingMachine.DAL.Entities
{
    public class VMCreator : BaseEntity<Guid>
    {
        [Required]
        public Guid VMEntityId { get; set; }
        public VMEntity VMEntity { get; set; }

        [Required]
        public string Name { get; set; }
        public int Price { get; set; }
        public int Availability { get; set; }

        public TypeProduct TypeProduct { get; set; }

        /// <summary>
        /// Factory creator class name
        /// </summary>
        [Required]
        public string CreatorClassName { get; set; }
    }
}
