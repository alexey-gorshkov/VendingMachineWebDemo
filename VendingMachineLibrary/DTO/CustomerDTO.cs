using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.BLL.DTO
{
    public class CustomerDTO
    {
        // purse customer
        public PurseDTO PurseCustomer { get; set; }

        // deposit amount user
        public int AmountDeposited { get; set; }
    }
}
