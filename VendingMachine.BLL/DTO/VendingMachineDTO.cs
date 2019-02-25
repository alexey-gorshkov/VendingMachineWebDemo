using System.Collections.Generic;

namespace VendingMachine.BLL.DTO
{
    public class VendingMachineDTO
    {
        // available product creators
        public IEnumerable<CreatorProductDTO> CreatorProducts { get; set; }

        // purse VM
        public PurseDTO Purse { get; set; }
    }
}
