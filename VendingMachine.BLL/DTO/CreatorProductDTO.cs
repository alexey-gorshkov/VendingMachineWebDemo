using System.ComponentModel.DataAnnotations;
using VendingMachine.Core.Models;

namespace VendingMachine.BLL.DTO
{
    public class CreatorProductDTO
    {
        public int Availability { get; set; }
        [Required]
        public TypeProduct TypeProduct { get; set; }
        public ProductDTO Product { get; set; }
    }
}