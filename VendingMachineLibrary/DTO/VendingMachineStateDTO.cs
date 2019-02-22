using System.Collections.Generic;

namespace VendingMachine.BLL.DTO
{
    public class VendingMachineStateDTO
    {
        public CustomerDTO Customer { get; set; }

        public VendingMachineDTO VendingMachine { get; set; }
    }
}