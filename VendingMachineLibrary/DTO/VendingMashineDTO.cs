using System.Collections.Generic;

namespace VendingMachine.BLL.DTO
{
    public class VendingMachineDTO
    {
        // purse VM
        public PurseDTO PurseVM { get; set; }

        // purse user
        public PurseDTO PurseUser { get; set; }

        // available produc creators
        public IEnumerable<CreatorProductDTO> Creators { get; set; }

        // deposit amount user
        public int AmountDeposited { get; set; }
    }
}