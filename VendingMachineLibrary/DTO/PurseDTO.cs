using System.Collections.Generic;

namespace VendingMachine.BLL.DTO
{
    public class PurseDTO
    {
        public IEnumerable<CoinCountDTO> Coins { get; set; }
    }
}