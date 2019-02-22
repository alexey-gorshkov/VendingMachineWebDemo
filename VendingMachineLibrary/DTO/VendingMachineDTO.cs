using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.BLL.DTO
{
    public class VendingMachineDTO
    {
        // available product creators
        public IEnumerable<CreatorProductDTO> Creators { get; set; }

        // purse VM
        public PurseDTO PurseVM { get; set; }
    }
}
