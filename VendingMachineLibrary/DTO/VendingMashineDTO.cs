using System.Collections.Generic;

namespace VendingMachine.BLL.DTO
{
    public class VendingMachineStateDTO
    {
        // purse customer
        public PurseDTO PurseCustomer { get; set; }

        // deposit amount user
        public int AmountDeposited { get; set; }

        // available produc creators
        public IEnumerable<CreatorProductDTO> Creators { get; set; }

        // purse VM
        public PurseDTO PurseVM { get; set; }
    }
}