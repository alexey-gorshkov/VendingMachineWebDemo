using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.BLL.DTO
{
    public class CustomerProductDTO
    {
        public ProductDTO Product { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
